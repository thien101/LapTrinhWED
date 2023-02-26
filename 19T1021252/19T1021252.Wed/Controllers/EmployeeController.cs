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
    public class EmployeeController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string EMPLOYEE_SEARCH = "EmployeeCondition";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /*public ActionResult Index(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfEmployees(page, PAGE_SIZE, searchValue, out rowCount);

            int pageCount = (int)Math.Round((double)rowCount / PAGE_SIZE);

            ViewBag.Page = page;
            ViewBag.RowCount = rowCount;
            ViewBag.PageCount = pageCount;
            ViewBag.SearchValue = searchValue;

            return View(data);
        }*/

        public ActionResult Index()
        {
            Models.PaginationSearchInput condition = Session[EMPLOYEE_SEARCH] as Models.PaginationSearchInput;

            if(condition == null)
            {
                condition = new Models.PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }
            return View(condition);
        }

        
        public ActionResult Search(Models.PaginationSearchInput condition)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfEmployees(condition.Page,
                                                        condition.PageSize,
                                                        condition.SearchValue,
                                                        out rowCount);
            Models.EmployeeSearchOutput result = new Models.EmployeeSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };

            Session[EMPLOYEE_SEARCH] = condition;

            return View(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Employee()
            {
                EmployeeID = 0,
            };
            ViewBag.Title = "Bổ sung nhân viên";
            return View("Edit", data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            if (id <= 0)
                return RedirectToAction("Index");

            var data = CommonDataService.GetEmployee(id);
            if (data == null)
                return RedirectToAction("index");

            ViewBag.Title = "Cập nhập nhân viên";
            return View(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Employee data, string birthday, HttpPostedFileBase uploadPhoto)
        {
            //Kiểm soát dữ liệu đầu vào
            DateTime? d = Converter.DMYStringtoDateTime(birthday);
            if (d == null)
                ModelState.AddModelError("BirthDate", "Phải nhập ngày sinh");
            else
                data.BirthDate = d.Value;

            if (string.IsNullOrWhiteSpace(data.FirstName))
                ModelState.AddModelError(nameof(data.FirstName), "Họ không được để trống");
            if (string.IsNullOrWhiteSpace(data.LastName))
                ModelState.AddModelError(nameof(data.LastName), "Tên không được để trống");
            if (string.IsNullOrWhiteSpace(data.Email))
                ModelState.AddModelError(nameof(data.Email), "Email không được trống");

            data.Notes = data.Notes ?? "";

            if(ModelState.IsValid == false)
            {
                ViewBag.Title = data.EmployeeID == 0 ? "Bổ sung nhân viên" : "Cập nhập nhân viên";
                return View("Edit",data);
            }

            if(uploadPhoto != null)
            {
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string path = Server.MapPath("~/Images/Employees");
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                data.Photo = $"Images/Employees/{fileName}";
            }

            if (data.EmployeeID == 0)
            {
                CommonDataService.AddEmployee(data);
            } else
            {
                CommonDataService.UpdateEmployee(data);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            if (id <= 0)
                return RedirectToAction("Index");

            if(Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteEmployee(id);
                return RedirectToAction("Index");
            }

            var data = CommonDataService.GetEmployee(id);
            if (data == null)
                return RedirectToAction("Index");
            return View(data);
        }
    }
}