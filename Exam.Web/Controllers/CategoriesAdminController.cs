using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam.Web.Models;
using Kendo.Mvc.Extensions;
using Exam.Models;

namespace Exam.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesAdminController : BaseController
    {
        
        // GET: /CategoriesAdmin/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ReadCategories([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Categories.All()
                .Select(CategoriesViewModel.FromCategory);


            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateCategory([DataSourceRequest] DataSourceRequest request, CategoriesViewModel category)
        {
            if (category != null && ModelState.IsValid)
            {
                var newCategory = new Category
                {
                   Name = category.Name
                };

                this.Data.Categories.Add(newCategory);
                this.Data.SaveChanges();

                category.Id = newCategory.Id;
            }

            return Json(new[] { category }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateCategories([DataSourceRequest] DataSourceRequest request, CategoriesViewModel category)
        {
            var commentDb = this.Data.Categories.GetById(category.Id);

            commentDb.Name = category.Name;
            this.Data.SaveChanges();

            return Json(new[] { category }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DestroyCategories([DataSourceRequest] DataSourceRequest request, CategoriesViewModel category)
        {
            Category newcat = new Category
            {
                Id = category.Id,
                Name = category.Name
            };
            this.Data.Categories.Delete(newcat);
            this.Data.SaveChanges();

            return Json(new[] { category }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
	}
}