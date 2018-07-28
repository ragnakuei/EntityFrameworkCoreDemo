using System;
using System.Collections.Generic;
using System.Linq;
using EntityFrameworkCoreDemo.IBLL;
using EntityFrameworkCoreDemo.Models.Shared;
using EntityFrameworkCoreDemo.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCoreDemo.Controllers
{
    public class CountyController : Controller
    {
        private readonly ICountyBLL                _bll;
        private readonly ILogger<CountyController> _logger;
        private readonly UserInfo                  _userInfo;

        public CountyController(ICountyBLL                bll,
                                ILogger<CountyController> logger,
                                UserInfo                  userInfo)
        {
            _userInfo = userInfo;
            _bll      = bll;
            _logger   = logger;
        }

        public IActionResult Index()
        {
            var data = _bll.Get();
            return View(data);
        }

        public IActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var county = _bll.Get(id);
            if (county == null)
                return NotFound();

            return View(county);
        }

        public IActionResult Create()
        {
            ViewBag.CountryId = GetCountryList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CountyVM countyVm)
        {
            if (ModelState.IsValid)
            {
                _bll.Add(countyVm);
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = GetCountryList();
            return View(countyVm);
        }

        public IActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var county = _bll.Get(id);
            if (county == null)
                return NotFound();

            ViewBag.CountryId = GetCountryList();
            return View(county);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CountyVM county)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Update(county);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    ViewBag.CountryId = GetCountryList();
                    return View(county);
                }

                return RedirectToAction("Index");
            }

            ViewBag.CountryId = GetCountryList();
            return View(county);
        }

        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var county = _bll.Get(id);
            if (county == null)
            {
                return NotFound();
            }

            return View(county);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _bll.Del(id);
            return RedirectToAction("Index");
        }

        private IEnumerable<SelectListItem> GetCountryList()
        {
            var result = _bll.GetIdAndCurrentLanguageNames(_userInfo.CurrentLanguage)
                             .Select(cl => new SelectListItem
                                           {
                                               Value = cl.CountryId.ToString(),
                                               Text  = cl.Name
                                           });
            return result;
        }
    }
}