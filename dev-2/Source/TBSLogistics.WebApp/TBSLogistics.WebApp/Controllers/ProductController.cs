using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TBSLogistics.WebApp.Areas.Admin.Repository;
using TBSLogistics.WebApp.Models;

namespace TBSLogistics.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;
        public ProductController()
        {
            _productRepository = new ProductRepository(new UnitOfWork<TBSDBEntities>());
        }
        public ActionResult Index(int productID)
        {
            var product = (from p in _productRepository.GetAll() select p)
                .Where(x => x.ID == productID).FirstOrDefault();
            return View(product);
        }
    }
}