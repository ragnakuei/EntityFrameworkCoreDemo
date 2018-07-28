﻿using System;
using EntityFrameworkCoreDemo.IBLL;
using EntityFrameworkCoreDemo.Log;
using EntityFrameworkCoreDemo.Models.Shared;
using EntityFrameworkCoreDemo.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkCoreDemo.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryBLL _bll;
        private readonly LogAdapter  _logAdapter;
        private readonly UserInfo    _userInfo;

        public CountryController(ICountryBLL bll, LogAdapter logAdapter, UserInfo userInfo)
        {
            _userInfo   = userInfo;
            _bll        = bll;
            _logAdapter = logAdapter;
            _logAdapter.Initial(GetType().Name);
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

            var country = _bll.Get(id);
            if (country == null)
                return NotFound();

            return View(country);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CountryVM countryVm)
        {
            if (ModelState.IsValid)
            {
                _bll.Add(countryVm);
                return RedirectToAction("Index");
            }

            return View(countryVm);
        }

        public IActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var country = _bll.Get(id);
            if (country == null)
                return NotFound();

            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CountryVM country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Update(country);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    return View(country);
                }

                return RedirectToAction("Index");
            }

            return View(country);
        }

        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var country = _bll.Get(id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _bll.Del(id);
            return RedirectToAction("Index");
        }
    }
}