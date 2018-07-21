using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFrameworkCoreDemo.BLL.IBLL;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkCoreDemo.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryBLL _bll;

        public CountryController(ICountryBLL bll)
        {
            _bll = bll;
        }

        public IActionResult Index()
        {
            var data = _bll.Get();
            return View(data);
        }
    }
}