using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EntityFrameworkCoreDemo.BLL.IBLL;
using EntityFrameworkCoreDemo.DAL.IDAL;
using EntityFrameworkCoreDemo.Models.EntityModel;
using EntityFrameworkCoreDemo.Models.ViewModel;

namespace EntityFrameworkCoreDemo.BLL
{
    public class CountryBLL : ICountryBLL
    {
        private readonly ICountryDAL _dal;
        private readonly string _currentLanguage;

        public CountryBLL(ICountryDAL dal)
        {
            _dal = dal;
            _currentLanguage = Thread.CurrentThread.CurrentUICulture.ToString();
        }

        public List<CountryVM> Get()
        {
            var vms = _dal.Get()
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
                                        .FirstOrDefault(cl => cl.Language == _currentLanguage);
            result.Language = _currentLanguage;
            if (countryLanguage != null)
            {
                result.LanguageId = countryLanguage.CountryLanguageId;
                result.Name       = countryLanguage.Name;
            }

            return result;
        }
    }
}