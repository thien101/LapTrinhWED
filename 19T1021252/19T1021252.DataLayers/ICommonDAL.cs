using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021252.DomainModels;

namespace _19T1021252.DataLayers
{
    /// <summary>
    /// Định nghĩ các phép dữ liêu chung
    /// </summary>
    public interface ICommonDAL<T> where T : class
    {
        /// <summary>
        /// Tìm kiếm và hiển thị danh sách dữ liệu dưới dạng phân trang(pagination)
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng hiền thị trên mỗi trang(bằng 0 nếu ko phân trang)</param>
        /// <param name="searchValue">Giá trị cần tìm(chuổi rỗng nếu ko tìm kiếm)</param>
        /// <returns></returns>
        IList<T> List(int page = 1, int pageSize = 0, string searchValue = "");

        /// <summary>
        /// đếm tống số kết quả tìm dc
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue = "");

        /// <summary>
        /// Lấy 1 dòng dữ liệu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        /// Bổ sung dữ liệu và CSDL
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ID dữ liêu vừa bổ sung</returns>
        int Add(T data);

        /// <summary>
        /// Cập nhập dữ liệu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(T data);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// Kiểm tra xem hiện có dữ liệu khác liên quan đến dữ liệu có mã là id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool InUsed(int id);
    }
}
