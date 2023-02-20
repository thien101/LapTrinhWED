using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace _19T1021252.DataLayers.SQLServer
{
    /// <summary>
    /// Lớp cở sở cho các lớp cài đặt chức năng xử lý dữ liệu trên SQL
    /// </summary>
    public abstract class _BaseDAL
    {
        /// <summary>
        /// Tham số kết nối với CSDL
        /// </summary>
        protected string _connectionString;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="connectionString"></param>
        public _BaseDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Tạo và mở kết nối đến cơ sở dử liệu
        /// </summary>
        /// <returns></returns>
        protected SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();
            return connection;
        }
    }
}
