using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TBSLogistics.WebApp.Areas.Admin.Repository;
using TBSLogistics.WebApp.Models;

namespace TBSLogistics.WebApp.Areas.Admin
{
    public class WebShopAuthorizeAttribute: AuthorizeAttribute
    {
        private readonly string[] allowedroles;
        private IUserRepository _userRepository;
        private IUnitOfWork<TBSDBEntities> _unitOfWork = new UnitOfWork<TBSDBEntities>();
        public WebShopAuthorizeAttribute()
        {

        }
        public WebShopAuthorizeAttribute(params string[] roles)
        {
            this.allowedroles = roles;
            _userRepository = new UserRepository(_unitOfWork);
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            if (httpContext?.User != null)
            {
               var user = (from u in _userRepository.GetAll() select u)
                    .Where(x => x.Email == httpContext.User.Identity.Name).FirstOrDefault();
                if (user != null)
                {
                    var roles = user.AspNetRoles.Where(x => allowedroles.Contains(x.Name)).ToArray();
                    if (roles.Count() > 0)
                    {
                        authorize = true;
                    }

                }
            }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}