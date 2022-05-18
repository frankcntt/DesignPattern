using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TBSLogistics.WebApp.Areas.Admin.Repository;
using TBSLogistics.WebApp.Models;

namespace TBSLogistics.WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [WebShopAuthorizeAttribute("Admin")]
    public class RoleController : Controller
    {
        // GET: Admin/Role
        private IRoleRepository _roleRepository;
        public RoleController()
        {
            _roleRepository = new RoleRepository(new UnitOfWork<TBSDBEntities>());
        }
        public ActionResult Index()
        {
            var roles = _roleRepository.GetAll();
            return View(roles);
        }
    }
}