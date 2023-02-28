using _19T1021252.DataLayers;
using _19T1021252.DomainModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021252.BusinessLayers
{
    /// <summary>
    /// Các chức năng tác nghiệp liên quan đến tài khoản
    /// </summary>
    public static class UserAccountService
    {
        private static IUserAccountDAL employeeAccountDB;
        private static IUserAccountDAL customerAccountDB;

        static UserAccountService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            employeeAccountDB = new DataLayers.SQLServer.EmployeeAccountDAL(connectionString);
            customerAccountDB = new DataLayers.SQLServer.CustomerAccountDAL(connectionString);

        }

        public static UserAccount Authorize(AccountTypes accounType, string userName, string password)
        {
            if(accounType == AccountTypes.Employee)
            {
                return employeeAccountDB.Authorize(userName, password);
            }
            else
            {
                return customerAccountDB.Authorize(userName, password);
            }
        }

        public static bool ChangePassword(AccountTypes accountType, string userName, string oldPassword, string newPassword)
        {
            if(accountType == AccountTypes.Employee)
            {
                return employeeAccountDB.ChangePassword(userName, oldPassword, newPassword);
            }
            else
            {
                return customerAccountDB.ChangePassword(userName, oldPassword, newPassword);
            }
        }
    }
}
