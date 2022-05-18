using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TBSLogistics.WebApp.Areas.Admin.Repository;
using TBSLogistics.WebApp.Models;

namespace TBSLogistics.WebApp.Controllers
{
    public class ShopController : Controller
    {
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private IUnitOfWork<TBSDBEntities> _unitOfWork = new UnitOfWork<TBSDBEntities>();
        public ShopController()
        {
            _productRepository = new ProductRepository(_unitOfWork);
            _categoryRepository = new CategoryRepository(_unitOfWork);
        }
        // GET: Shop
        public ActionResult Index(int? cateID)
        {
            var categories = from c in _categoryRepository.GetAll() select c;
            var result = categories.Where(x => x.ParentID == cateID).ToList();
            ViewBag.cateID = cateID;
            return View(result);
        }
        public ActionResult GetProducts(int? cateID)
        {
            var categoryModel = from c in _categoryRepository.GetAll() select c;
            var productModel = from p in _productRepository.GetAll() select p;
            var categories = categoryModel.Where(x => x.ParentID == cateID).ToList();
            var result = new List<Tbs_Product>();
            if (categories.Count() > 0)
            {
                foreach (var cate in categories)
                {
                    result.AddRange(productModel
                        .Where(x => x.CategoryID == cate.ID).ToList()
                        );
                }
            }
            else result = productModel.Where(x => x.CategoryID == cateID).ToList();

            return PartialView("_partialBodyProducts", result);
        }
    }
}