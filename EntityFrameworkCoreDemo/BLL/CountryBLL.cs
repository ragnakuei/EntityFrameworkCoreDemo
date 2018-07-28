using System;
using System.Collections.Generic;
using System.Linq;
using EntityFrameworkCoreDemo.IBLL;
using EntityFrameworkCoreDemo.IDAL;
using EntityFrameworkCoreDemo.Models.EntityModel;
using EntityFrameworkCoreDemo.Models.Shared;
using EntityFrameworkCoreDemo.Models.ViewModel;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCoreDemo.BLL
{
    public class CountryBLL : ICountryBLL
    {
        private readonly ICountryDAL         _countryDal;
        private readonly UserInfo            _userInfo;
        private readonly ILogger<CountryBLL> _logger;

        public CountryBLL(ICountryDAL         countryDal,
                          UserInfo            userInfo,
                          ILogger<CountryBLL> logger)
        {
            _countryDal = countryDal;
            _userInfo   = userInfo;
            _logger     = logger;
        }

        public List<CountryVM> Get()
        {
            var vms = _countryDal.Get()
                                 .Select(c => ToCountryVM(c))
                                 .ToList();
            return vms;
        }

        private CountryVM ToCountryVM(Country entity)
        {
            var result = new CountryVM();
            result.Id   = entity.CountryId;
            result.Code = entity.Code;

            var countryLanguage = entity.CountryLanguages
                                        .FirstOrDefault(cl => cl.Language == _userInfo.CurrentLanguage);
            result.Language = _userInfo.CurrentLanguage;
            if (countryLanguage != null)
            {
                result.LanguageId = countryLanguage.CountryLanguageId;
                result.Name       = countryLanguage.Name;
            }

            return result;
        }

        public CountryVM Get(Guid id)
        {
            var entity = _countryDal.Get(id);
            var result = ToCountryVM(entity);
            return result;
        }

        public bool Add(CountryVM countryVm)
        {
            var entity = new Country();
            entity = ToCountryInsertEntity(countryVm);
            return _countryDal.Add(entity);
        }

        private Country ToCountryInsertEntity(CountryVM countryVm)
        {
            Country entity;
            entity = new Country
                     {
                         CountryId = Guid.NewGuid(),
                         Code      = countryVm.Code
                     };
            entity.CountryLanguages = new List<CountryLanguage>
                                      {
                                          new CountryLanguage
                                          {
                                              CountryLanguageId = Guid.NewGuid(),
                                              Language          = _userInfo.CurrentLanguage,
                                              Name              = countryVm.Name,
                                              //CountryId         = entity.CountryId   // 可以不用預先給定
                                          }
                                      };
            return entity;
        }

        public bool Update(CountryVM countryVm)
        {
            var result = new Country
                         {
                             CountryId        = countryVm.Id,
                             Code             = countryVm.Code,
                             CountryLanguages = new List<CountryLanguage>()
                         };

            var item = new CountryLanguage();
            item.CountryId         = countryVm.Id;
            item.Name              = countryVm.Name;
            item.Language          = countryVm.Language   ?? _userInfo.CurrentLanguage;
            item.CountryLanguageId = countryVm.LanguageId ?? Guid.NewGuid();
            result.CountryLanguages.Add(item);
            return _countryDal.Update(result);
        }

        public bool Del(Guid id)
        {
            return _countryDal.Delete(id);
        }
    }
}