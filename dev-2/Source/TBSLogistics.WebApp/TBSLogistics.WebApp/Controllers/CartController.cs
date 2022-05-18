using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TBSLogistics.WebApp.Areas.Admin.Repository;
using TBSLogistics.WebApp.DTO;
using TBSLogistics.WebApp.Models;

namespace TBSLogistics.WebApp.Controllers
{
    public class CartController : Controller
    {
        private IUserRepository _userRepository;
        private ICartRepository _cartRepository;
        private ICartItemRepository _cartItemRepository;
        private IProductRepository _productRepository;
        private IOrderRepository _orderRepository;
        private IUnitOfWork<TBSDBEntities> _unitOfWork = new UnitOfWork<TBSDBEntities>();
        public CartController()
        {
            _userRepository = new UserRepository(_unitOfWork);
            _cartRepository = new CartRepository(_unitOfWork);
            _cartItemRepository = new CartItemRepository(_unitOfWork);
            _productRepository = new ProductRepository(_unitOfWork);
            _orderRepository = new OrderRepository(_unitOfWork);
        }
        public ActionResult Index()
        {
            var result = new CartViewModel();
            int totalQty = 0;
            double totalPrice = 0;
            if (User.Identity.IsAuthenticated)
            {
                var user = (from s in _userRepository.GetAll() select s)
                    .Where(x => x.Email == User.Identity.Name.Trim()).FirstOrDefault();
                var cartItems = (from c in _cartItemRepository.GetAll()
                                 join b in _cartRepository.GetAll()
                                 on c.CartID equals b.ID
                                 select new { c, b })
                    .Where(x => x.b.UserID == user.Id
                    && x.b.IsCheckout == false).ToList();
                var cart = (from b in _cartRepository.GetAll() select b)
                    .Where(x => x.UserID == user.Id && x.IsCheckout == false).FirstOrDefault();
                if (cart == null)
                {
                    //If cart is empty. So i will create new cart
                    _cartRepository.Add(cart = new Tbs_Cart
                    {
                        UserID = user.Id,
                        IsCheckout = false
                    });
                    _unitOfWork.Save();
                }
                result.CartID = cart.ID;
                foreach (var item in cartItems)
                {
                    var cartItem = new CartItemViewModel
                    {
                        Product = new Tbs_Product
                        {
                            ID = (int)item.c.ProductID
                            ,
                            Name = item.c.Tbs_Product.Name
                            ,
                            Image = item.c.Tbs_Product.Image
                            ,
                            Price = item.c.Price,
                            Inventory = item.c.Tbs_Product.Inventory
                        },
                        HistoricalCost = item.c.Tbs_Product.Price,
                        Qty = (int)item.c.Qty
                    };
                    totalQty += (int)item.c.Qty;
                    totalPrice += (double)item.c.Price;
                    result.CartItems.Add(cartItem);
                }
                result.TotalPrice = totalPrice;
                result.TotalQty = totalQty;

            }
            else
            {
                result = ((CartViewModel)Session["cart"])
                    ?? new CartViewModel();
                foreach (var item in result.CartItems)
                {
                    totalPrice += (double)item.Product.Price;
                    totalQty += item.Qty;
                }
                result.TotalPrice = totalPrice;
                result.TotalQty = totalQty;
            }
            return View(result);
        }
        public JsonResult Cancel(int orderID, string cancelReason)
        {
            var user = (from s in _userRepository.GetAll() select s)
                  .Where(x => x.Email == User.Identity.Name.Trim()).FirstOrDefault();
            var cart = (from o in _orderRepository.GetAll()
                        join a in _cartRepository.GetAll() on o.CartID equals a.ID
                        where o.ID == orderID
                        && a.IsCheckout == true
                        && o.Status == 1
                        select a).FirstOrDefault();
            if (cart == null) return Json(new {success = false, message = "Đơn hàng này đã được xác nhận và bạn không thể hủy. Vui lòng liên hệ cửa hàng!"});
            //cart.IsCheckout = false;
            //_cartRepository.Update(cart);
            var order = (from o in _orderRepository.GetAll() select o)
                 .Where(x => x.ID == orderID).FirstOrDefault();
            if (order != null)
            {
                order.IsCancel = true;
                order.CancelBy = User.Identity.Name;
                order.CancelDate = DateTime.Now;
                order.CancelReason = cancelReason;
                _orderRepository.Update(order);
            }
            _unitOfWork.Save();
            return Json(new {success = true});
        }
        public JsonResult CountItem()
        {
            int count = 0;
            if (User.Identity.IsAuthenticated)
            {
                var user = (from s in _userRepository.GetAll() select s)
                    .Where(x => x.Email == User.Identity.Name.Trim()).FirstOrDefault();
                count = (from c in _cartItemRepository.GetAll()
                         join b in _cartRepository.GetAll()
                         on c.CartID equals b.ID
                         select b)
                  .Where(x => x.UserID == user.Id
                  && x.IsCheckout == false).Count();
            }
            else count = ((CartViewModel)Session["cart"]) == null
                    ? 0 : ((CartViewModel)Session["cart"]).CartItems.Count;

            return Json(count, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Buy(int id, int? qty = 1)
        {
            int cartItemCount = 0;
            var product = (from s in _productRepository.GetAll() select s)
                .Where(x => x.ID == id).FirstOrDefault();
            if (User.Identity.IsAuthenticated)
            {
                var user = (from s in _userRepository.GetAll() select s)
                                .Where(x => x.Email == User.Identity.Name.Trim()).FirstOrDefault();
                var cartItems = (from c in _cartItemRepository.GetAll()
                                 join b in _cartRepository.GetAll()
                                 on c.CartID equals b.ID
                                 where c.ProductID == id && b.UserID == user.Id && b.IsCheckout == false
                                 select new { c, b }).FirstOrDefault();
                if (cartItems == null)
                {
                    var cart = (from c in _cartRepository.GetAll() select c)
                        .Where(x => x.UserID == user.Id && x.IsCheckout == false).FirstOrDefault();
                    if (cart == null) _cartRepository.Add(cart = new Tbs_Cart
                    {
                        UserID = user.Id,
                        IsCheckout = false
                    });
                    _unitOfWork.Save();
                    _cartItemRepository.Add(new Tbs_CartItem
                    {
                        ProductID = id,
                        CartID = cart.ID,
                        Qty = qty,
                        Price = product.Price
                    });
                    _unitOfWork.Save();

                }
                else
                {
                    cartItems.c.Qty += qty;
                    _cartItemRepository.Update(cartItems.c);
                }
                _unitOfWork.Save();
                cartItemCount = (from c in _cartItemRepository.GetAll()
                                 join b in _cartRepository.GetAll()
                                 on c.CartID equals b.ID
                                 where b.UserID == user.Id && b.IsCheckout == false
                                 select new { Id = c.ID }).Count();
            }
            else
            {
                var cart = new CartViewModel();
                if (Session["cart"] == null)
                {

                    cart.CartItems.Add(new CartItemViewModel
                    {
                        Product = product
                        ,
                        HistoricalCost = product.Price,
                        Qty = 1,
                    });

                    Session["cart"] = cart;
                }
                else
                {
                    cart = (CartViewModel)Session["cart"];
                    int index = IsExist(id);
                    if (index != -1)
                    {
                        cart.CartItems[index].Qty++;
                    }
                    else
                    {
                        cart.CartItems.Add(new CartItemViewModel
                        {
                            Product = product
                        ,
                            HistoricalCost = product.Price,
                            Qty = 1,
                        });
                    }
                    Session["cart"] = cart;
                    cartItemCount = cart.CartItems.Count();
                }
            }
            return Json(new { cartItemCount });
        }

        public JsonResult Remove(int id)
        {

            if (User.Identity.IsAuthenticated)
            {
                var user = (from s in _userRepository.GetAll() select s)
                    .Where(x => x.Email == User.Identity.Name.Trim()).FirstOrDefault();
                var cartItems = (from c in _cartItemRepository.GetAll()
                                 join b in _cartRepository.GetAll()
                                 on c.CartID equals b.ID
                                 where c.ProductID == id && b.UserID == user.Id && b.IsCheckout == false
                                 select c).FirstOrDefault();
                if (cartItems != null) _cartItemRepository.Delete(cartItems);
                _unitOfWork.Save();
            }
            else
            {
                var cart = (CartViewModel)Session["cart"];
                if (cart != null)
                {
                    int index = IsExist(id);
                    cart.CartItems.RemoveAt(index);
                    Session["cart"] = cart;
                }
            }
            return Json(true);
        }
        public JsonResult UpdateQty(int id, int qty)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = (from s in _userRepository.GetAll() select s)
                     .Where(x => x.Email == User.Identity.Name.Trim()).FirstOrDefault();
                var cartItems = (from c in _cartItemRepository.GetAll()
                                 join b in _cartRepository.GetAll()
                                 on c.CartID equals b.ID
                                 where c.ProductID == id && b.UserID == user.Id && b.IsCheckout == false
                                 select c).FirstOrDefault();
                cartItems.Qty = qty;
                cartItems.Price = cartItems.Tbs_Product.Price * qty;
                _cartItemRepository.Update(cartItems);
                _unitOfWork.Save();
            }
            else
            {
                var cart = (CartViewModel)Session["cart"];
                if (cart != null)
                {
                    int index = IsExist(id);
                    var cartItem = cart.CartItems[index];
                    cartItem.Qty = qty;
                    cartItem.Product.Price = cartItem.HistoricalCost * qty;
                    Session["cart"] = cart;
                }

            }

            return Json(true);
        }

        private int IsExist(int id)
        {
            var cart = (CartViewModel)Session["cart"];
            for (int i = 0; i < cart.CartItems.Count; i++)
                if (cart.CartItems[i].Product.ID.Equals(id))
                    return i;
            return -1;
        }

    }
}