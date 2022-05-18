using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TBSLogistics.WebApp.Areas.Admin.DTO;
using TBSLogistics.WebApp.Areas.Admin.Repository;
using TBSLogistics.WebApp.DTO;
using TBSLogistics.WebApp.Models;

namespace TBSLogistics.WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [WebShopAuthorizeAttribute("Admin")]
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

        /// <summary>
        /// list order
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            return View();
        }

        /// <summary>
        /// detail order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(int id)
        {
            var order = (from o in _orderRepository.GetAll()
                         join u in _userRepository.GetAll() on o.UserID equals u.Id
                         where o.ID == id
                         select new OrderDetailViewModel
                         {
                             OrderId = o.ID,
                             OrderRequestBy = u.Email,
                             OrderDate = o.OrderDate,
                             OrderStatus = o.Status,
                             TotalPrice = o.TotalPrice,
                             TotalQty = o.TotalQty,
                             NumberPhone = u.PhoneNumber,
                             Address = u.Address,
                             IsCancel = o.IsCancel,
                             CancelBy = o.CancelBy,
                             CancelDate = o.CancelDate,
                             CancelReason = o.CancelReason,
                             OrderProducts = o.Tbs_Cart.Tbs_CartItem.Select(x => new OrderProduct
                             {
                                 Image = x.Tbs_Product.Image,
                                 Name = x.Tbs_Product.Name,
                                 Qty = x.Qty,
                                 Price = x.Price
                             }).ToList(),
                         }).FirstOrDefault();
            return PartialView("_partialDetailOrder", order);
        }

        /// <summary>
        /// Load list Order by ajax
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadOrders()
        {
            var order = (from o in _orderRepository.GetAll()
                         join u in _userRepository.GetAll() on o.UserID equals u.Id
                         select new OrderViewModel
                         {
                             OrderId = o.ID,
                             OrderRequestBy = u.Email,
                             OrderDate = o.OrderDate,
                             OrderStatus = o.Status,
                             TotalPrice = o.TotalPrice,
                             TotalQty = o.TotalQty,
                             IsCancel = o.IsCancel
                         }).OrderByDescending(x => x.OrderDate).ToList();

            return PartialView("_partialOrderTable", order);
        }

        /// <summary>
        /// Admin excute order
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public JsonResult ExcuteOrder(int orderID, int type)
        {
            var order = (from o in _orderRepository.GetAll() select o)
                .Where(x => x.ID == orderID).FirstOrDefault();
            if(order != null) order.Status = type;
            if (type == 4) order.CompleteDate = DateTime.Now;
            _orderRepository.Update(order);
            _unitOfWork.Save();
            return Json(new {success = true, message ="Success!"});
        }

        /// <summary>
        /// Get a order to cancel
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public ActionResult GetOrderByCancel(int orderID)
        {
            var order = (from o in _orderRepository.GetAll() select o)
               .Where(x => x.ID == orderID).FirstOrDefault();
            ViewBag.OrderID = order.ID;
            return PartialView("_partialOrderCancelReason");
        }

        /// <summary>
        /// Cancel order by admin
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="cancelReason"></param>
        /// <returns></returns>
        public JsonResult Cancel(int orderID, string cancelReason)
        {
            var order = (from o in _orderRepository.GetAll() select o)
                 .Where(x => x.ID == orderID).FirstOrDefault();
            if (order != null)
            {
                order.IsCancel = true;
                order.CancelBy = User.Identity.Name;
                order.CancelDate = DateTime.Now;
                order.CancelReason = cancelReason;
                _orderRepository.Update(order);
                _unitOfWork.Save();
            }
            return Json(new { success = true, message ="Cancel Success!" });
        }
       
    }
}