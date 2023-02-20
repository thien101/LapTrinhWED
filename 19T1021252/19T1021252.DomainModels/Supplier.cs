using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021252.DomainModels
{
    /// <summary>
    /// Supplier
    /// </summary>
    public class Supplier
    {
        /// <summary>
        /// Mã nhà cung cấp
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// Tên liên lạc
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Thành phố
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Đất nước
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Số diện thoại
        /// </summary>
        public string Phone { get; set; }
    }
}
