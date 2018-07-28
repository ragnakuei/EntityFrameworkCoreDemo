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
    public class CvController : Controller
    {
        private readonly ICvBLL                _cvBLL;
        private readonly ILogger<CvController> _logger;
        private readonly UserInfo              _userInfo;

        public CvController(ICvBLL                cvBLL,
                            ILogger<CvController> logger,
                            UserInfo              userInfo)
        {
            _userInfo = userInfo;
            _cvBLL    = cvBLL;
            _logger   = logger;
        }

        public IActionResult Index()
        {
            var data = _cvBLL.Get();
            ViewBag.CountryDict = GetCountryDict();
            ViewBag.CountyDict  = GetCountyDict();
            return View(data);
        }

        public IActionResult Create()
        {
            var vm = new CompCvVM
                     {
                         Certificates         = Enumerable.Repeat(new CompCvCertificateVM(), 2).ToList(),
                         Educations           = Enumerable.Repeat(new CompCvEducationVM(), 2).ToList(),
                         LanguageRequirements = Enumerable.Repeat(new CompCvLanguageRequirementVM(), 2).ToList()
                     };
            ViewBag.Language  = GetLanguageOptions();
            ViewBag.Listening = GetLanguageRequirementOptions();
            ViewBag.CountryId = GetCountryOptions();
            ViewBag.CountyId  = GetCountyOptions();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CompCvVM cvVm)
        {
            if (ModelState.IsValid)
            {
                _cvBLL.Add(cvVm);
                return RedirectToAction("Index");
            }

            ViewBag.Language  = GetLanguageOptions();
            ViewBag.Listening = GetLanguageRequirementOptions();
            ViewBag.CountryId = GetCountryOptions();
            ViewBag.CountyId  = GetCountyOptions();
            return View(cvVm);
        }

        public IActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var county = _cvBLL.Get(id);
            if (county == null)
                return NotFound();

            ViewBag.Language  = GetLanguageOptions();
            ViewBag.Listening = GetLanguageRequirementOptions();
            ViewBag.CountryId = GetCountryOptions();
            ViewBag.CountyId  = GetCountyOptions();
            return View(county);
        }


        public IActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var compCv = _cvBLL.Get(id);
            if (compCv == null)
                return NotFound();

            compCv.Certificates.Add(new CompCvCertificateVM());
            compCv.Educations.Add(new CompCvEducationVM());

            ViewBag.Language  = GetLanguageOptions();
            ViewBag.Listening = GetLanguageRequirementOptions();
            ViewBag.CountryId = GetCountryOptions();
            ViewBag.CountyId  = GetCountyOptions();
            return View(compCv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CompCvVM compCv)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _cvBLL.Update(compCv);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    ViewBag.Language  = GetLanguageOptions();
                    ViewBag.Listening = GetLanguageRequirementOptions();
                    ViewBag.CountryId = GetCountryOptions();
                    ViewBag.CountyId  = GetCountyOptions();
                    return View(compCv);
                }

                return RedirectToAction("Index");
            }

            ViewBag.Language  = GetLanguageOptions();
            ViewBag.Listening = GetLanguageRequirementOptions();
            ViewBag.CountryId = GetCountryOptions();
            ViewBag.CountyId  = GetCountyOptions();
            return View(compCv);
        }

        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var county = _cvBLL.Get(id);
            if (county == null)
            {
                return NotFound();
            }

            ViewBag.Language  = GetLanguageOptions();
            ViewBag.Listening = GetLanguageRequirementOptions();
            ViewBag.CountryId = GetCountryOptions();
            ViewBag.CountyId  = GetCountyOptions();
            return View(county);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _cvBLL.Del(id);
            return RedirectToAction("Index");
        }

        private Dictionary<Guid, string> GetCountryDict()
        {
            var result = _cvBLL.GetCountryIdNames(_userInfo.CurrentLanguage)
                               .ToDictionary(k => k.CountryId, v => v.Name);
            return result;
        }

        private Dictionary<Guid, string> GetCountyDict()
        {
            var result = _cvBLL.GetCountyIdNames(_userInfo.CurrentLanguage)
                               .ToDictionary(k => k.CountyId, v => v.Name);
            return result;
        }

        private IEnumerable<SelectListItem> GetCountryOptions()
        {
            var result = _cvBLL.GetCountryIdNames(_userInfo.CurrentLanguage)
                               .Select(c => new SelectListItem
                                            {
                                                Text  = c.Name,
                                                Value = c.CountryId.ToString(),
                                            });
            return result;
        }

        private IEnumerable<SelectListItem> GetCountyOptions()
        {
            var result = _cvBLL.GetCountyIdNames(_userInfo.CurrentLanguage)
                               .Select(c => new SelectListItem
                                            {
                                                Text  = c.Name,
                                                Value = c.CountyId.ToString(),
                                            });
            return result;
        }

        private List<SelectListItem> GetLanguageRequirementOptions()
        {
            return new List<SelectListItem>
                   {
                       new SelectListItem {Text = "不會", Value = "0",},
                       new SelectListItem {Text = "略懂", Value = "1",},
                       new SelectListItem {Text = "中等", Value = "2",},
                       new SelectListItem {Text = "精通", Value = "3",},
                   };
        }

        private List<SelectListItem> GetLanguageOptions()
        {
            return new List<SelectListItem>
                   {
                       new SelectListItem {Text = "中文", Value = "0",},
                       new SelectListItem {Text = "英文", Value = "1",},
                   };
        }
    }
}