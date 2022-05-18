using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TBSLogistics.WebApp.Areas.Admin.Repository;
using TBSLogistics.WebApp.Models;

namespace TBSLogistics.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private ICategoryRepository _categoryRepository;
        public HomeController()
        {
            _categoryRepository = new CategoryRepository(new UnitOfWork<TBSDBEntities>());
        }
        public ActionResult Index()
        {
            var Categories = from c in  _categoryRepository.GetAll() select c;
            var result = Categories.Where(x => x.IsActive == true && x.ParentID == null).ToList();
            return View(result);
        }

       
    }
}