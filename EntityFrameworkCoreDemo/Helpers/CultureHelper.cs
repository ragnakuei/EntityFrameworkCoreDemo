using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using EntityFrameworkCoreDemo.Enums;
using Microsoft.AspNetCore.Http;

namespace EntityFrameworkCoreDemo.Helpers
{
    public class CultureHelper
    {
        public class Culture
        {
            public int    Key   { get; set; }
            public string Value { get; set; }
        }

        public static string GetImplementedCulture(string culture)
        {
            if (string.IsNullOrWhiteSpace(culture) == false
                && GetAllImplementedCultures().Any(x => x.Value.Equals(culture)) )
                return culture;

            return GetDefaultCulture();
        }

        public static IEnumerable<Culture> GetAllImplementedCultures()
        {
            return Enum.GetValues(typeof(CultureEnum))
                       .Cast<CultureEnum>()
                       .Select(cl => new Culture
                                     {
                                         Key   = cl.ToIntValue(),
                                         Value = cl.GetDescription()
                                     });
        }

        public static void SetCulture(HttpContext httpContext, string language)
        {
            var culture       = GetImplementedCulture(language);
            httpContext.Response.Cookies.Append("_culture",
                                                culture,
                                                new CookieOptions
                                                {
                                                    Expires  = DateTime.Now.AddMonths(2),
                                                    HttpOnly = true
                                                });

            var ci = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture   = ci;
        }

        public static string GetDefaultCulture()
        {
            return CultureEnum.en_US.GetDescription(); //預設為en-US 英文
        }
    }
}