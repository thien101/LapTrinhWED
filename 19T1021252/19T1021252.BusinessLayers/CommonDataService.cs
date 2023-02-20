using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021252.DomainModels;
using _19T1021252.DataLayers;
using System.Configuration;


namespace _19T1021252.BusinessLayers
{
    /// <summary>
    /// Cung cấp chứng năng nghiệp vụ xủ lý dữ liệu chung liên quan:
    /// Quốc gia, Nhà cung câp, khách hàng, Người giao hàng, Nhân viên, Loại Hàng
    /// </summary>
    public static class CommonDataService
    {
        private static ICommonDAL<Supplier> supplierDB;
        private static ICountryDAL countryDB;
        private static ICommonDAL<Category> categoryDB;
        private static ICommonDAL<Customer> customerDB;
        private static ICommonDAL<Shipper> shipperDB;
        private static ICommonDAL<Employee> employeeDB;

        /// <summary>
        /// Ctor
        /// </summary>
        static CommonDataService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            countryDB = new DataLayers.SQLServer.CountryDAL(connectionString);

            supplierDB = new DataLayers.SQLServer.SupplierDAL(connectionString);
            categoryDB = new DataLayers.SQLServer.CategoryDAL(connectionString);
            customerDB = new DataLayers.SQLServer.CustomerDAL(connectionString);
            shipperDB = new DataLayers.SQLServer.ShipperDAL(connectionString);
            employeeDB = new DataLayers.SQLServer.EmployeeDAL(connectionString);
        }

        #region Xử lý liên quan đên quốc gia
        /// <summary>
        /// Danh sách các quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListOfCountries()
        {
            return countryDB.List().ToList();
        }
        #endregion

        #region Xử lý liên quan đến supplier
        /// <summary>
        /// Tìm kiếm , lấy danh sách nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pagrSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Giá trị tìm kiếm</param>
        /// <param name="rowCount">Tham số đầu ra số dòng dữ liệu tìm dc</param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(int page, int pagrSize, string searchValue, out int rowCount)
        {
            rowCount = supplierDB.Count(searchValue);
            return supplierDB.List(page, pagrSize, searchValue).ToList();
        }

        /// <summary>
        /// Hiện thị hết tất cả danh sách ncc không phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagrSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(string searchValue = "")
        {
            return supplierDB.List().ToList();
        }

        /// <summary>
        /// Lấy thông tin 1 nhà cung cấp dưới vào mã, trả về data của ncc
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Supplier GetSupplier(int supplierID)
        {
            return supplierDB.Get(supplierID);
        }

        /// <summary>
        /// Bổ sung nhà cung cấp. Hàm trả về mã của ncc
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static int AddSupplier(Supplier data)
        {
            return supplierDB.Add(data);
        }

        /// <summary>
        /// Cập nhập 1 nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);
        }

        /// <summary>
        /// Xóa nhà cung cấp bằng mã
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool DeleteSupplier(int supplierID)
        {
            return supplierDB.Delete(supplierID);
        }

        /// <summary>
        /// kiểm tra nhà cung cấp đã tồn tại hay chưa bằng mã
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool InUsedSupplier(int supplierID)
        {
            return supplierDB.InUsed(supplierID);
        }
        #endregion

        #region Xử lý liên quan đến Category
        /// <summary>
        /// Tìm kiếm , lấy danh sách loại hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pagrSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Giá trị tìm kiếm</param>
        /// <param name="rowCount">Tham số đầu ra số dòng dữ liệu tìm dc</param>
        /// <returns></returns>
        public static List<Category> ListOfCategories(int page, int pagrSize, string searchValue, out int rowCount)
        {
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pagrSize, searchValue).ToList();
        }

        /// <summary>
        /// Hiện thị hết tất cả danh sách loại hàng không phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagrSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Category> ListOfCategories(string searchValue = "")
        {
            return categoryDB.List().ToList();
        }

        /// <summary>
        /// Lấy thông tin 1 loại hàng bằng mã, trả về data của ncc
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Category GetCategory(int categoryID)
        {
            return categoryDB.Get(categoryID);
        }

        /// <summary>
        /// Bổ sung loại hàng. Hàm trả về mã của ncc
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static int AddCategory(Category data)
        {
            return categoryDB.Add(data);
        }

        /// <summary>
        /// Cập nhập 1 loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }

        /// <summary>
        /// Xóa loại hàng bằng mã
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool DeleteCategory(int categoryID)
        {
            return categoryDB.Delete(categoryID);
        }

        /// <summary>
        /// kiểm tra loại hàng đã tồn tại hay chưa bằng mã
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool InUsedCategory(int categoryID)
        {
            return categoryDB.InUsed(categoryID);
        }
        #endregion

        #region Xử lý liên quan đến Customer
        /// <summary>
        /// Tìm kiếm , lấy danh sách khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pagrSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Giá trị tìm kiếm</param>
        /// <param name="rowCount">Tham số đầu ra số dòng dữ liệu tìm dc</param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(int page, int pagrSize, string searchValue, out int rowCount)
        {
            rowCount = customerDB.Count(searchValue);
            return customerDB.List(page, pagrSize, searchValue).ToList();
        }

        /// <summary>
        /// Hiện thị hết tất cả danh sách khách hàng không phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagrSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(string searchValue = "")
        {
            return customerDB.List().ToList();
        }

        /// <summary>
        /// Lấy thông tin 1 khách hàng bằng mã, trả về data của ncc
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Customer GetCustomer(int CustomerID)
        {
            return customerDB.Get(CustomerID);
        }

        /// <summary>
        /// Bổ sung khách hàng. Hàm trả về mã của ncc
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static int AddCustomer(Customer data)
        {
            return customerDB.Add(data);
        }

        /// <summary>
        /// Cập nhập 1 khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(Customer data)
        {
            return customerDB.Update(data);
        }

        /// <summary>
        /// Xóa khách hàng bằng mã
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool DeleteCustomer(int CustomerID)
        {
            return customerDB.Delete(CustomerID);
        }

        /// <summary>
        /// kiểm tra khách hàng đã tồn tại hay chưa bằng mã
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool InUsedCustomer(int CustomerID)
        {
            return customerDB.InUsed(CustomerID);
        }
        #endregion

        #region Xử lý liên quan đến Employee
        /// <summary>
        /// Tìm kiếm, lấy danh sách nhân viên dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pagrSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Giá trị tìm kiếm</param>
        /// <param name="rowCount">Tham số đầu ra số dòng dữ liệu tìm dc</param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(int page, int pagrSize, string searchValue, out int rowCount)
        {
            rowCount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pagrSize, searchValue).ToList();
        }

        /// <summary>
        /// Tìm kiếm, lấy hết tất cả danh sách nhân viên không phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagrSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(string searchValue = "")
        {
            return employeeDB.List().ToList();
        }

        /// <summary>
        /// Lấy thông tin 1 nhân viên bằng mã, trả về data của nhân viên
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Employee GetEmployee(int employeeID)
        {
            return employeeDB.Get(employeeID);
        }

        /// <summary>
        /// Bổ sung nhân viên. Hàm trả về mã của nhân viên
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static int AddEmployee(Employee data)
        {
            return employeeDB.Add(data);
        }

        /// <summary>
        /// Cập nhập 1 nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }

        /// <summary>
        /// Xóa nhân viên bằng mã
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool DeleteEmployee(int employeeID)
        {
            return employeeDB.Delete(employeeID);
        }

        /// <summary>
        /// kiểm tra nhân viên đã tồn tại hay chưa bằng mã
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool InUsedEmployee(int employeeID)
        {
            return employeeDB.InUsed(employeeID);
        }
        #endregion

        #region Xử lý liên quan đến Shipper
        /// <summary>
        /// Tìm kiếm , lấy danh sách shipper dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pagrSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Giá trị tìm kiếm</param>
        /// <param name="rowCount">Tham số đầu ra số dòng dữ liệu tìm dc</param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(int page, int pagrSize, string searchValue, out int rowCount)
        {
            rowCount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pagrSize, searchValue).ToList();
        }

        /// <summary>
        /// Tìm kiếm , Hiện thị hết tất cả danh sách shipper không phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagrSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(string searchValue = "")
        {
            return shipperDB.List().ToList();
        }

        /// <summary>
        /// Lấy thông tin 1 shipper bằng mã, trả về data của shipper
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Shipper GetShipper(int shipperID)
        {
            return shipperDB.Get(shipperID);
        }

        /// <summary>
        /// Bổ sung shipper. Hàm trả về mã của shipper
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static int AddShipper(Shipper data)
        {
            return shipperDB.Add(data);
        }

        /// <summary>
        /// Cập nhập 1 shipper
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }

        /// <summary>
        /// Xóa shipper bằng mã
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool DeleteShipper(int shipperID)
        {
            return shipperDB.Delete(shipperID);
        }

        /// <summary>
        /// kiểm tra shipper đã tồn tại hay chưa bằng mã
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool InUsedShipper(int shipperID)
        {
            return shipperDB.InUsed(shipperID);
        }
        #endregion
    }
}