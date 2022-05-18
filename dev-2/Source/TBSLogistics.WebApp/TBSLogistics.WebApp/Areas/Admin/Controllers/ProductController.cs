using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TBSLogistics.WebApp.Areas.Admin.Repository;
using TBSLogistics.WebApp.Areas.Admin.Utilities;
using TBSLogistics.WebApp.Models;

namespace TBSLogistics.WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [WebShopAuthorizeAttribute("Admin")]
    public class ProductController : Controller
    {
        private string _message;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private IUnitOfWork<TBSDBEntities> _unitOfWork = new UnitOfWork<TBSDBEntities>();
        public ProductController()
        {
            _message = string.Empty;
            _productRepository = new ProductRepository(_unitOfWork);
            _categoryRepository = new CategoryRepository(_unitOfWork);
        }
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetProductTable(int categoryId)
        {
            var products = from p in _productRepository.GetAll() select p;
            var categories = from c in _categoryRepository.GetAll() select c;
            var category = categories.Where(x => x.ID == categoryId).FirstOrDefault();
            var result = new List<Tbs_Product>();
            if (category.ParentID != null) result = products.Where(x => x.CategoryID == categoryId).ToList();
            else
            {
                var subCategories = categories.Where(x => x.ParentID == category.ID).ToList();
                foreach (var subCate in subCategories)
                {
                    result.AddRange(products.Where(x => x.CategoryID == subCate.ID).ToList());
                }
            }
            return PartialView("_partialProductsTable", result);
        }
        public ActionResult GetProductToUpdate(int Id)
        {
            var products = from p in _productRepository.GetAll() select p;
            return PartialView("_partialUpdateProduct", 
                (products.Where(x => x.ID == Id).FirstOrDefault()));
        }
        #region Action Product(Add, Update, Delete)
        /// <summary>
        /// Add Product
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult Add(ProductViewModel fileData)
        {
            var data = fileData;
            var products = from p in _productRepository.GetAll() select p;
            if ((products
                .Where(x => x.Name == data.Name.Trim())
                .FirstOrDefault()) != null)
                return Json(new { success = false, message = "The product is existed on the system.Please check again!" });
            string img_path = String.Empty;
            HttpPostedFileBase file = fileData.File;
            if (file != null && file.ContentLength > 0)
            {
                string name = file.FileName.Trim().ToLower();
                bool isExists = System.IO.Directory.Exists(Server.MapPath(WebConstants.IMAGE_PATH_PRODUCT));
                if (!isExists) System.IO.Directory.CreateDirectory(Server.MapPath(WebConstants.IMAGE_PATH_PRODUCT));
                img_path = Path.Combine(WebConstants.IMAGE_PATH_PRODUCT, name);
                file.SaveAs(Server.MapPath(img_path));
            }
            else
            {
                _message = "The image is empty.Please check again!";
                return Json(new { success = false, message = _message });
            }
            var product = new Tbs_Product
            {
                Name = fileData.Name.Trim(),
                Description = fileData.Description.Trim(),
                Price = fileData.Price,
                SKU = fileData.SKU,
                Inventory = fileData.Inventory,
                VAT = fileData.VAT,
                IsFreeShip = fileData.IsFreeShip,
                CategoryID = fileData.CategoryID,
                CreateBy = User.Identity.Name,
                CreateDate = DateTime.Now,
                Image = img_path,
                IsActve  = true
            };
            _productRepository.Add(product);
            _unitOfWork.Save();
            _message = "Success!";
            return Json(new { success = true, message = _message });
        }
        public ActionResult Detail(int Id)
        {
            var products = from p in _productRepository.GetAll() select p;
            return PartialView("_partialDetailProduct",
                (products.Where(x => x.ID == Id).FirstOrDefault()));
        }

        [HttpGet]
        public ActionResult Update(int Id)
        {
            var products = from p in _productRepository.GetAll() select p;
            return PartialView("_partialUpdateProduct",
                (products.Where(x => x.ID == Id).FirstOrDefault()));
        }

        [HttpPost]
        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult Update(ProductViewModel fileData)
        {
            var data = fileData;
            var products = from p in _productRepository.GetAll() select p;
            var product = products.Where(x => x.ID == data.ID).FirstOrDefault();
            if (product == null) return Json(new { success = false, message = "an error occurred while updating.Please check again!" });
            string img_path = String.Empty;
            HttpPostedFileBase file = fileData.File;
            if (file != null && file.ContentLength > 0)
            {
                string name = file.FileName.Trim().ToLower();
                bool isExists = System.IO.Directory.Exists(Server.MapPath(WebConstants.IMAGE_PATH_PRODUCT));
                if (!isExists) System.IO.Directory.CreateDirectory(Server.MapPath(WebConstants.IMAGE_PATH_PRODUCT));
                img_path = Path.Combine(WebConstants.IMAGE_PATH_PRODUCT, name);
                file.SaveAs(Server.MapPath(img_path));
            }
            product.Name = fileData.Name.Trim();
            product.Description = fileData.Description.Trim();
            product.Price = fileData.Price;
            product.SKU = fileData.SKU;
            product.Inventory = fileData.Inventory;
            product.VAT = fileData.VAT;
            product.IsFreeShip = fileData.IsFreeShip;
            product.Last_Updated_Date = DateTime.Now;
            product.IsActve = fileData.IsActve;
            if (!string.IsNullOrEmpty(img_path)) product.Image = img_path;
            _productRepository.Update(product);
            _unitOfWork.Save();
            _message = "Success!";
            return Json(new { success = true, message = _message });
        }

        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult Delete(int Id)
        {
            var products = from p in _productRepository.GetAll() select p;
            var product = products.Where(x => x.ID == Id).FirstOrDefault();
            if (product == null) return Json(new { success = false, message = "an error occurred while updating.Please check again!" });
            _productRepository.Delete(product);
            _unitOfWork.Save();
            _message = "Success!";
            return Json(new { success = true, message = _message });
        }

        #endregion
    }
}