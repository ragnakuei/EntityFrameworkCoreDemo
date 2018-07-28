using System;
using System.Collections.Generic;
using EntityFrameworkCoreDemo.Models.EntityModel;
using EntityFrameworkCoreDemo.Models.ViewModel;

namespace EntityFrameworkCoreDemo.IBLL
{
    public interface ICountyBLL
    {
        List<CountyVM>               Get();
        CountyVM                     Get(Guid        id);
        bool                         Add(CountyVM    countyVm);
        bool                         Update(CountyVM countyVm);
        bool                         Del(Guid        id);
        IEnumerable<CountryLanguage> GetIdAndCurrentLanguageNames(string currentLanguage);
    }
}