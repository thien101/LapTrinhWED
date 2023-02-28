using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021252.DomainModels;

namespace _19T1021252.DataLayers
{
    /// <summary>
    /// Định nghĩa cách phép xử lý dữ liệu liên quan đến tài khoản
    /// </summary>
    public interface IUserAccountDAL
    {
        /// <summary>
        /// Kiểm tra xem tên đăng nhập và mật khẩu có hợp lệ?
        /// Nếu hợp lệ trả về thông tin người dùng
        /// ngược lại thì null
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserAccount Authorize(string userName, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ChangePassword(string userName, string oldPassword, string newPassword);

    }
}
