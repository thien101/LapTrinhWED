using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021252.DomainModels;

namespace _19T1021252.DataLayers
{
    /// <summary>
    /// Định nghĩa phép xử lý dữ liệu liên quan đến quốc gia
    /// </summary>
    public interface ICountryDAL
    {
        IList<Country> List();
    }
}
