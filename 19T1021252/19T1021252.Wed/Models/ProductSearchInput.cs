using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021252.Wed.Models
{
    public class ProductSearchInput : PaginationSearchInput
    {
        public int SupplierID { get; set; }

        public int CategoryID { get; set; }
    }
}