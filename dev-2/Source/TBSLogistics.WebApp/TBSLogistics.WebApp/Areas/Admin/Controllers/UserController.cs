using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TBSLogistics.WebApp.Areas.Admin.Repository;
using TBSLogistics.WebApp.Models;

namespace TBSLogistics.WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [WebShopAuthorizeAttribute("Admin")]
    public class UserController : Controller
    {
        private string _message;
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IUnitOfWork<TBSDBEntities> _unitOfWork = new UnitOfWork<TBSDBEntities>();
        public UserController()
        {
            _roleRepository = new RoleRepository(_unitOfWork);
            _userRepository = new UserRepository(_unitOfWork);
        }

        public UserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _message = string.Empty;
 
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
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        // GET: Admin/User
        public ActionResult Index()
        {
            var usr = UserManager.Users.ToList();
            ViewBag.Roles = _roleRepository.GetAll();
            return View(usr);
        }
        public async Task<JsonResult> Add(RegisterViewModel model)
        {

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var users = from s in _userRepository.GetAll()
                            select s;
                var usr = users.Where(x => x.Email == model.Email).FirstOrDefault();
                usr.PhoneNumber = model.PhoneNumber;
                usr.Address = model.Address;
                usr.CreateBy = User.Identity.Name;
                usr.CreateDate = DateTime.Now;
                usr.IsValid = true;
                usr.IsDelete = false;
                _userRepository.Update(usr);
                _unitOfWork.Save();

                if (!(await UserManager.IsInRoleAsync(usr.Id, model.Role)))
                    await UserManager.AddToRoleAsync(usr.Id, model.Role);
                _message = "New User Success!";
                return Json(new { success = true,message = _message }, JsonRequestBehavior.AllowGet);
            }

            // If we got this far, something failed, redisplay form
            _message = "An error occurred while Adding. Please check again!";
            return Json(new { success = false, _message }, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Edit(string id)
        {
            var usr = (from u in _userRepository.GetAll() select u)
                .Where(x => x.Id == id).Select(x => new RegisterViewModel
                {
                    Email = x.Email,
                    Address = x.Address,
                    PhoneNumber = x.PhoneNumber,
                    Role = x.AspNetRoles.FirstOrDefault() != null 
                    ? x.AspNetRoles.FirstOrDefault().Name : null,
                }).FirstOrDefault();
            ViewBag.Roles = _roleRepository.GetAll();
            return PartialView("_partialUpdateUser",usr);
        }
        public ActionResult GetUserToResetPass(string id)
        {
            var usr = (from u in _userRepository.GetAll() select u)
                .Where(x => x.Id == id).FirstOrDefault();
            ViewBag.Email = usr.Email;
            return PartialView("_partialResetPassword");
        }

        public async Task<JsonResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await UserManager.FindByNameAsync(model.Email);
            var token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            var result = await UserManager.ResetPasswordAsync(user.Id, token, model.Password);
            if (result.Succeeded)
            {
                _message = "Update Success!";
                return Json(new { success = true, message = _message }, JsonRequestBehavior.AllowGet);
            }
            _message = "Update Failed!";
            return Json(new { success = false, message = _message }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Update(RegisterViewModel model)
        {

            var usr = (from s in _userRepository.GetAll()
                        select s).Where(x => x.Email == model.Email).FirstOrDefault();

            usr.PhoneNumber = model.PhoneNumber;
            usr.Address = model.Address;
            _userRepository.Update(usr);
            _unitOfWork.Save();
            //await UserManager.RemoveFromRolesAsync(usr.Id, model.Role);
            var roles = await UserManager.GetRolesAsync(usr.Id);
            await UserManager.RemoveFromRolesAsync(usr.Id, roles.ToArray());
            if (!(await UserManager.IsInRoleAsync(usr.Id, model.Role)))
                await UserManager.AddToRoleAsync(usr.Id, model.Role);
            _message = "Update Success!";
            return Json(new { success = true, message = _message }, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
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
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "You have entered the wrong email or password.");
                    return View(model);
            }
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home", new { Area = "Admin" });
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Order", new {Area = "Admin" });
        }

    }
}