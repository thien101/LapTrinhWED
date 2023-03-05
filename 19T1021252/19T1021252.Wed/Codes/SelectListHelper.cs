﻿using System;
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

        public static List<SelectListItem> Suppliers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "0",
                Text = "-- Loại hàng --",
            });

            foreach (var item in CommonDataService.ListOfSuppliers())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.SupplierId.ToString(),
                    Text = item.SupplierName
                });
            }

            return list;
        }

        public static List<SelectListItem> Categories()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "0",
                Text = "-- Nhà cung cấp --",
            });

            foreach (var item in CommonDataService.ListOfCategories())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CategoryID.ToString(),
                    Text = item.CategoryName
                });
            }

            return list;
        }
    }
}