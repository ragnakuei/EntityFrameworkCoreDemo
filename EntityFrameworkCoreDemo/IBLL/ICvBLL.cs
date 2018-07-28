using System;
using System.Collections.Generic;
using EntityFrameworkCoreDemo.Models.EntityModel;
using EntityFrameworkCoreDemo.Models.ViewModel;

namespace EntityFrameworkCoreDemo.IBLL
{
    public interface ICvBLL
    {
        List<CompCvVM> Get();
        IEnumerable<CountryLanguage> GetCountryIdNames(string currentLanguage);
        IEnumerable<CountyLanguage> GetCountyIdNames(string currentLanguage);
        void Add(CompCvVM cvVm);
        CompCvVM Get(Guid id);
        bool Update(CompCvVM cvVm);
        bool Del(Guid id);
    }
}