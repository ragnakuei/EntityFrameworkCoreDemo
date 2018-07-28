using System.Threading;
using EntityFrameworkCoreDemo.Helpers;
using Microsoft.AspNetCore.Http;

namespace EntityFrameworkCoreDemo.Models.Shared
{
    public class UserInfo
    {
        public UserInfo(IHttpContextAccessor httpContext)
        {
            CurrentLanguage = httpContext.HttpContext?.Request.Cookies["_culture"]
                                ?? Thread.CurrentThread.CurrentUICulture.ToString();
            DefaultLanguage = CultureHelper.GetDefaultCulture();
        }

        public string CurrentLanguage { get; }

        public string DefaultLanguage { get; }
    }
}