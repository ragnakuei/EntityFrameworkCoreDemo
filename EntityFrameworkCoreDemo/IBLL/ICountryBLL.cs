using System;
using System.Collections.Generic;
using EntityFrameworkCoreDemo.Models.ViewModel;

namespace EntityFrameworkCoreDemo.IBLL
{
    public interface ICountryBLL
    {
        List<CountryVM> Get();
        CountryVM       Get(Guid         id);
        bool            Add(CountryVM    countryVm);
        bool            Update(CountryVM countryVm);
        bool            Del(Guid         id);
    }
}