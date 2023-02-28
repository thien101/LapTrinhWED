
using _19T1021252.DomainModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace _19T1021252.Wed
{
    public class Converter
    {
        public static DateTime? DMYStringtoDateTime(string s, string format = "d/M/yyyy")
        {
            try
            {
                return DateTime.ParseExact(s, format, CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        public static UserAccount CookieToUserAccount(string value)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<UserAccount>(value);
        }
    }
}