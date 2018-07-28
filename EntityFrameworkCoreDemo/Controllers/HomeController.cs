using System.Diagnostics;
using System.Linq;
using EntityFrameworkCoreDemo.Helpers;
using EntityFrameworkCoreDemo.Models;
using EntityFrameworkCoreDemo.Models.Shared;
using EntityFrameworkCoreDemo.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EntityFrameworkCoreDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserInfo _userInfo;

        public HomeController(UserInfo userInfo)
        {
            _userInfo = userInfo;
        }

        public IActionResult Index()
        {
            ViewBag.CurrentLanguage = _userInfo.CurrentLanguage;

            ViewData["LangSelectList"] = CultureHelper.GetAllImplementedCultures()
                                                      .Select(c =>
                                                              {
                                                                  var item = new SelectListItem();
                                                                  item.Text     = c.Value;
                                                                  item.Value    = c.Value;
                                                                  item.Selected = c.Value.ToLower() == _userInfo.CurrentLanguage.ToLower();
                                                                  return item;
                                                              });

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Set(CultureVM cultureVm)
        {
            CultureHelper.SetCulture(HttpContext, cultureVm.Language);
            return RedirectToAction("Index");
        }
    }
}