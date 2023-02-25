using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021252.DomainModels;
using _19T1021252.BusinessLayers;

namespace _19T1021252.Wed.Controllers
{
    public class SupplierController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string SUPPLIER_SEARCH = "SupplierCondition";
        /*/// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfSuppliers(page, PAGE_SIZE,searchValue, out rowCount);
            int pageCount = (int)Math.Round((double)rowCount / PAGE_SIZE);

            //ViewData["page"] = page; Truyền dữ liệu bằng ViewData

            ViewBag.Page = page;//Truyền dữ liệu bằng ViewBag
            ViewBag.RowCount = rowCount;
            ViewBag.PageCount = pageCount;
            ViewBag.SearchValue = searchValue;
            return View(data);//Truyền dữ liệu bằng model
        }*/

        public ActionResult Index()
        {
            Models.PaginationSearchInput condition = Session[SUPPLIER_SEARCH] as Models.PaginationSearchInput;

            if(condition == null)
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

        /// <summary>
        /// Tìm kiếm và hiển thị danh sách dưới dạng phân trang
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Search(Models.PaginationSearchInput condition)//int Page. int PageSize, string searchvalue
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfSuppliers(condition.Page,
                                                        condition.PageSize,
                                                        condition.SearchValue, 
                                                        out rowCount);

            Models.SupplierSearchOutput resutl = new Models.SupplierSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };

            Session[SUPPLIER_SEARCH] = condition;

            return View(resutl);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Supplier()
            {
                SupplierId = 0
            };
            ViewBag.Title = "Bổ sung nhà cung cấp";
            return View("Edit", data);
        }   

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction("Index");

            var data = CommonDataService.GetSupplier(id);
            if (data == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhập nhà cung cấp";
            return View(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// 
        [ValidateAntiForgeryToken] //Xac dinh nguon data co tu form cua minh hay tu ngoai vao
        [HttpPost]
        public ActionResult Save(Supplier data)
        {

            //Kiểm soat dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(data.SupplierName))
                ModelState.AddModelError(nameof(data.SupplierName), "Tên không được để rỗng");
            if (string.IsNullOrWhiteSpace(data.ContactName))
                ModelState.AddModelError(nameof(data.ContactName), "Tên giao dịch không được để trống");
            if (string.IsNullOrWhiteSpace(data.Country))
                ModelState.AddModelError(nameof(data.Country), "Vui lòng chọn quốc gia");

            data.Address = data.Address ?? "";
            data.Phone = data.Address ?? "";
            data.City = data.City ?? "";
            data.PostalCode = data.PostalCode ?? "";

            
            if(ModelState.IsValid == false)
            {
                ViewBag.Title = data.SupplierId == 0 ? "Bổ sung nhà cung cấp" : "Cập nhập nhà cung cấp";
                return View("Edit", data);
            }

            if(data.SupplierId == 0)
            {
                CommonDataService.AddSupplier(data);
            }
            else
            {
                CommonDataService.UpdateSupplier(data);
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

            if(Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteSupplier(id);
                return RedirectToAction("Index");
            }

            var data = CommonDataService.GetSupplier(id);
            if (data == null)
                return RedirectToAction("Index");
            
            return View(data);
        }
    }
}