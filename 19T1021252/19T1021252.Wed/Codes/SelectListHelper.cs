using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021252.BusinessLayers;
using _19T1021252.DomainModels;
using System.Web.Mvc;

namespace _19T1021252.Wed
{
    /// <summary>
    /// Danh sách quốc gia
    /// </summary>
    public class SelectListHelper
    {
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "-- Chọn quốc gia --",
            });

            foreach (var item in CommonDataService.ListOfCountries())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CountryName,
                    Text = item.CountryName
                });
            }

            return list;
        }
    }
}