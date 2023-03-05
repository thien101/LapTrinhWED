using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021252.DomainModels;

namespace _19T1021252.Wed.Models
{
    public class ProductSearchOutput : PaginationSearchOutput
    {
        public int SupplierID { get; set; }

        public int CategoryID { get; set; }

        public List<Product> Data { get; set; }
    }
}