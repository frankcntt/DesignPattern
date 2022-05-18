using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TBSLogistics.WebApp.Areas.Admin.Repository;
using TBSLogistics.WebApp.Areas.Admin.Utilities;
using TBSLogistics.WebApp.Models;

namespace TBSLogistics.WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [WebShopAuthorizeAttribute("Admin")]
    public class CategoryController : Controller
    {
        private string _message;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private IUnitOfWork<TBSDBEntities> _unitOfWork = new UnitOfWork<TBSDBEntities>();
        public CategoryController()
        {
            _message = string.Empty;
            _categoryRepository = new CategoryRepository(_unitOfWork);
            _productRepository = new ProductRepository(_unitOfWork);
        }
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View();
        }

        #region List Category
        /// <summary>
        /// List Category as table list
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public ActionResult GetCategoryTable(int categoryId)
        {
            var categories = from c in _categoryRepository.GetAll() select c;
            var result = categories.Where(x => x.ID == categoryId || x.ParentID == categoryId).ToList();
            return PartialView("_partialCategoriesTable", result);
        }
        /// <summary>
        /// Get Category  by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetCategory(int id)
        {
            var categories = from c in _categoryRepository.GetAll() select c;
            var result = categories.Where(x => x.ID == id).Select(x => new
            {
                ID = x.ID,
                Name = x.Name,
            }).FirstOrDefault();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Action Category(Add, Update, Delete)
        /// <summary>
        /// Add Parent Category(Dept Category)
        /// </summary>
        /// <param name="aName"></param>
        /// <param name="aDesc"></param>
        /// <returns></returns>
        public JsonResult Add(ProductViewModel fileData)
        {

            string img_path = String.Empty;
            HttpPostedFileBase file = fileData.File;
            if (file != null && file.ContentLength > 0)
            {
                string name = file.FileName.Trim().ToLower();
                bool isExists = System.IO.Directory.Exists(Server.MapPath(WebConstants.IMAGE_PATH_CATEGORY));
                if (!isExists) System.IO.Directory.CreateDirectory(Server.MapPath(WebConstants.IMAGE_PATH_CATEGORY));
                img_path = Path.Combine(WebConstants.IMAGE_PATH_CATEGORY, name);
                file.SaveAs(Server.MapPath(img_path));
            }
            else
            {
                _message = "The image is empty.Please check again!";
                return Json(new { success = false, message = _message });
            }
            var categories = from c in _categoryRepository.GetAll() select c;
            var category = categories.Where(x => x.Name == fileData.Name.Trim()).FirstOrDefault();
            if (category != null) return Json(new { success = false, message = "The Category is existed on the system.Please check again!" });
            category = new Tbs_Category
            {
                Name = fileData.Name.Trim(),
                Description = fileData.Description.Trim(),
                CreateDate = DateTime.Now,
                CreateBy = User.Identity.Name,
                Image = img_path,
                IsActive = true,
                ParentID = null
            };
            _categoryRepository.Add(category);
            _unitOfWork.Save();
            _message = "Success!";
            return Json(new {success = true, message = _message });
        }
        /// <summary>
        /// Add SubCategory
        /// </summary>
        /// <param name="aSubName"></param>
        /// <param name="aSubDesc"></param>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public JsonResult AddSubCategory(string aSubName, string aSubDesc, int ParentID)
        {

            var categories = from c in _categoryRepository.GetAll() select c;
            var category = categories.Where(x => x.Name == aSubName.Trim() && x.ParentID == ParentID).FirstOrDefault();
            if (category != null) return Json(new { success = false, message = "The Category is existed on the system.Please check again!" });
            category = new Tbs_Category
            {
                Name = aSubName.Trim(),
                Description = aSubDesc.Trim(),
                CreateDate = DateTime.Now,
                CreateBy = User.Identity.Name,
                IsActive = true,
                ParentID = ParentID
            };
            _categoryRepository.Add(category);
            _unitOfWork.Save();
            _message = "Success!";
            return Json(new { success = true, message = _message });
        }

        /// <summary>
        /// Get category to Update
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetCategoryToUpdate(int id)
        {
            var categories = from c in _categoryRepository.GetAll() select c;
            var result = categories.Where(x => x.ID == id).FirstOrDefault();
            return PartialView("_partialUpdateCategory", result);
        }

        /// <summary>
        /// Submit date update
        /// </summary>
        /// <param name="fileData"></param>
        /// <returns></returns>
        public JsonResult Update(CategoryViewModel fileData)
        {
            var categories = from c in _categoryRepository.GetAll() select c;
            var category = categories.Where(x => x.ID == fileData.ID).FirstOrDefault();
            if (category == null) return Json(new { success = false, message = "an error occurred while updating.Please check again!" });
            string img_path = String.Empty;
            HttpPostedFileBase file = fileData.File;
            if (file != null && file.ContentLength > 0)
            {
                string name = file.FileName.Trim().ToLower();
                bool isExists = System.IO.Directory.Exists(Server.MapPath(WebConstants.IMAGE_PATH_CATEGORY));
                if (!isExists) System.IO.Directory.CreateDirectory(Server.MapPath(WebConstants.IMAGE_PATH_CATEGORY));
                img_path = Path.Combine(WebConstants.IMAGE_PATH_CATEGORY, name);
                file.SaveAs(Server.MapPath(img_path));
            }
            category.Name = fileData.Name.Trim();
            category.Description = fileData.Description.Trim();
            category.Last_Updated_Date = DateTime.Now;
            if (!string.IsNullOrEmpty(img_path)) category.Image = img_path;
            category.IsActive = fileData.isActive;
            _categoryRepository.Update(category);
            _unitOfWork.Save();
            _message = "Success!";
            return Json(new { success = true, message = _message });
        }

        /// <summary>
        /// delete Category
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult Delete(int Id)
        {
            var categories = from c in _categoryRepository.GetAll() select c;
           
            var category = categories.Where(x => x.ID == Id).FirstOrDefault();
            if (category == null) return Json(new { success = false, message = "an error occurred while updating.Please check again!" });
            if(category.ParentID == null)
            {
                var subCategories = categories.Where(x => x.ParentID == category.ID).ToList();
                var produts = from p in _productRepository.GetAll() select p;
                foreach (var subCate in subCategories)
                {
                    // Delete all Product belog this Category
                    _productRepository.DeleteMultiple(produts.Where(x => x.CategoryID == subCate.ID));
                    _categoryRepository.Delete(subCate);
                }
                _unitOfWork.Save();
            }
            _categoryRepository.Delete(category);
            _unitOfWork.Save();
            _message = "Success!";
            return Json(new { success = true, message = _message });
        }
      
        #endregion
        #region Show Category as Tree Node
        /// <summary>
        /// Show category as tree node
        /// </summary>
        /// <returns></returns>
        public JsonResult CategoryTree()
        {
            return Json(Nodes(), JsonRequestBehavior.AllowGet);
        }

        private  IEnumerable<TreeCategoryViewModel> Nodes()
        {
            var categories = from c in _categoryRepository.GetAll()
                             select c;
            var deptCategory = categories.Where(x => x.ParentID == null).ToList();
            var nodes = deptCategory.Select(x => new TreeCategoryViewModel
            {
                id = x.ID,
                parent = "#",
                text = x.Name
            });
            var subCategory = categories.Where(x => x.ParentID != null).ToList();
            nodes = nodes.Concat(subCategory.Select(x => new TreeCategoryViewModel
            {
                id = x.ID,
                parent= x.ParentID.ToString(),
                text = x.Name
            }));
            return nodes;
        }
        #endregion
    }
}