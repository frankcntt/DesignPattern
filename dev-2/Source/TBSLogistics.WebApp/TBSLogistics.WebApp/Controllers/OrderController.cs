using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TBSLogistics.WebApp.Areas.Admin.Repository;
using TBSLogistics.WebApp.DTO;
using TBSLogistics.WebApp.Models;

namespace TBSLogistics.WebApp.Controllers
{[Authorize]
    public class OrderController : Controller
    {
        private IOrderRepository _orderRepository;
        private IUserRepository _userRepository;
        private ICartRepository _cartRepository;
        private ICartItemRepository _cartItemRepository;
        private IUnitOfWork<TBSDBEntities> _unitOfWork = new UnitOfWork<TBSDBEntities>();
        public OrderController()
        {
            _orderRepository = new OrderRepository(_unitOfWork);
            _userRepository = new UserRepository(_unitOfWork);
            _cartRepository = new CartRepository(_unitOfWork);
            _cartItemRepository = new CartItemRepository(_unitOfWork);
        }
        public ActionResult Index()
        {
            var user = (from s in _userRepository.GetAll() select s)
                .Where(x => x.Email == User.Identity.Name.Trim()).FirstOrDefault();
            var order = (from o in _orderRepository.GetAll()
                         join a in _cartRepository.GetAll() on o.CartID equals a.ID
                         where o.UserID == user.Id && a.IsCheckout == true
                         select o).Select(x => new OrderItem
                         {
                             OrderProducts = x.Tbs_Cart.Tbs_CartItem.Select(k => new OrderProduct
                             {
                                 ID = k.Tbs_Product.ID,
                                 Image = k.Tbs_Product.Image,
                                 Name = k.Tbs_Product.Name,
                                 Qty = k.Qty,
                                 Price = k.Price,
              
                             }).ToList(),
                             TotalPrice = x.TotalPrice,
                             TotalQty = x.TotalQty,
                             Status = x.Status,
                             OrderID = x.ID,
                             OrderDate = x.OrderDate,
                             IsCancel = x.IsCancel,
                             CancelBy = x.CancelBy,
                             CancelDate = x.CancelDate,
                             CancelReason = x.CancelReason
                         });
                        
            return View(order);
        }
        public ActionResult Checkout(int cartID)
        {
          var  user = (from s in _userRepository.GetAll() select s)
                .Where(x => x.Email == User.Identity.Name.Trim()).FirstOrDefault();
            var cartItems = (from c in _cartItemRepository.GetAll()
                             join b in _cartRepository.GetAll()
                             on c.CartID equals b.ID
                             where b.ID == cartID && b.IsCheckout == false
                             select c).ToList();
            double? totalPrice = 0;
            int? totalQty = 0;
            foreach (var item in cartItems)
            {
                totalPrice += item.Price;
                totalQty += item.Qty;
            }
            var order = new Tbs_Order
            {
                UserID = user.Id,
                CartID = cartID,
                TotalPrice = totalPrice,
                TotalQty = totalQty,
                OrderDate = DateTime.Now,
                IsCancel = false,
                Status = 1 // Chờ xác nhận
            };
            _orderRepository.Add(order);
            // update lại trạng thái ở cart
            var cart = (from c in _cartRepository.GetAll() select c)
                .Where(x => x.ID == cartID).FirstOrDefault();
            cart.IsCheckout = true;
            _cartRepository.Update(cart);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public ActionResult GetOrderByCancel(int orderID)
        {
            var order = (from o in _orderRepository.GetAll() select o)
               .Where(x => x.ID == orderID).FirstOrDefault();
            ViewBag.OrderID = order.ID;
            return PartialView("_partialOrderCancelReason");
        }
    }
}