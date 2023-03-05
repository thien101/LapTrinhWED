using _19T1021252.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021252.Wed.Models
{
    public class ProductEditModel : Product
    {
        public List<ProductAttribute> ListOfAttributes { get; set; }

        public List<ProductPhoto> ListOfPhoto { get; set; }

    }
}