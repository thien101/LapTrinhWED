using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021252.Wed.Models
{
    /// <summary>
    /// Lưu trữ thông tin đầu vào dùng để tìm kiếm, phân trang (đơn giản)
    /// </summary>
    public class PaginationSearchInput
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public string SearchValue { get; set; }

    }
}