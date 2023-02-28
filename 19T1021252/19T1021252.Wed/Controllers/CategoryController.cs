using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021252.BusinessLayers;
using _19T1021252.DataLayers;
using _19T1021252.DomainModels;

namespace _19T1021252.Wed.Controllers
{
    [Authorize]

    public class CategoryController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string CATEGORY_SEARCH = "CategoryCondition";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /*public ActionResult Index(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCategories(page, PAGE_SIZE, searchValue, out rowCount);

            int pageCount = (int)Math.Round((double)rowCount / PAGE_SIZE);

            ViewBag.Page = page;// truyền dữ liệu qua ViewBag
            ViewBag.RowCount = rowCount;
            ViewBag.PageCount = pageCount;
            ViewBag.SearchValue = searchValue;

            return View(data);//Truyền dữ liệu qua model
        }*/

        public ActionResult Index()
        {
            Models.PaginationSearchInput condition = Session[CATEGORY_SEARCH] as Models.PaginationSearchInput;

            if (condition == null)
            {
                condition = new Models.PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = "",
                };
            }

            return View(condition);
        }

        public ActionResult Search(Models.PaginationSearchInput condition)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCategories(condition.Page,
                                                            condition.PageSize,
                                                            condition.SearchValue,
                                                            out rowCount);

            Models.CategorySearchOutput result = new Models.CategorySearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data,
            };

            Session[CATEGORY_SEARCH] = condition;

            return View(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Category()
            {
                CategoryID = 0,
            };
            ViewBag.Title = "Bổ sung loại hàng";
            return View("Edit", data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if(id <= 0)
            {
                return RedirectToAction("Index");
            }
            var data = CommonDataService.GetCategory(id);
            if(data == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Chỉnh sửa loại hàng";
            return View(data);
        }

        public ActionResult Save(Category data)
        {
            if(data.CategoryID == 0)
            {
                CommonDataService.AddCategory(data);
            } else
            {
                CommonDataService.UpdateCategory(data);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction("Index");
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteCategory(id);
                return RedirectToAction("Index");
            }

            var data = CommonDataService.GetCategory(id);
            if (data == null)
                return RedirectToAction("Index");
            return View(data);
        }
    }
}