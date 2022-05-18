using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TBSLogistics.WebApp.Areas.Admin.Repository;
using TBSLogistics.WebApp.DTO;
using TBSLogistics.WebApp.Models;

namespace TBSLogistics.WebApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ICartRepository _cartRepository;
        private ICartItemRepository _cartItemRepository;
        private IUserRepository _userRepository;
        private IUnitOfWork<TBSDBEntities> _unitOfWork = new UnitOfWork<TBSDBEntities>();
        public AccountController()
        {
            _cartRepository = new CartRepository(_unitOfWork);
            _userRepository = new UserRepository(_unitOfWork);
            _cartItemRepository = new CartItemRepository(_unitOfWork);
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
           
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    var cartSession = ((CartViewModel)Session["cart"]) ?? new CartViewModel();
                    var user = (from s in _userRepository.GetAll() select s)
                        .Where(x => x.Email == model.Email).FirstOrDefault();
                    if(cartSession.CartItems.Count > 0)
                    {
                        foreach (var item in cartSession.CartItems)
                        {
                            var cartItems = (from c in _cartItemRepository.GetAll()
                                             join b in _cartRepository.GetAll()
                                             on c.CartID equals b.ID
                                             where c.ProductID == item.Product.ID && b.UserID == user.Id
                                             select c).FirstOrDefault();
                            if (cartItems != null) continue;
                            var cart = (from c in _cartRepository.GetAll() select c)
                                    .Where(x => x.UserID == user.Id).FirstOrDefault();
                            if (cart == null) _cartRepository.Add(cart = new Tbs_Cart
                            {
                                UserID = user.Id,
                                IsCheckout = false
                            });
                            _unitOfWork.Save();
                            _cartItemRepository.Add(new Tbs_CartItem
                            {
                                ProductID = item.Product.ID,
                                CartID = cart.ID,
                                Qty = 1,
                                Price = item.Product.Price
                            });
                            _unitOfWork.Save();
                        }
                    }
                    _unitOfWork.Save();
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "You have entered the wrong email or password.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var usr = (from u in _userRepository.GetAll() select u)
                        .Where(x => x.Email == model.Email).FirstOrDefault();
                    usr.PhoneNumber = model.PhoneNumber;
                    usr.Address = model.Address;
                    _userRepository.Update(usr);
                    _unitOfWork.Save();
                    await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

      
        //
        // POST: /Account/LogOff
        [HttpGet]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

       
        #endregion
    }
}