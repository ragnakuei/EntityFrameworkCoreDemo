using System.Collections.Generic;
using EntityFrameworkCoreDemo.Models.ViewModel;

namespace EntityFrameworkCoreDemo.BLL.IBLL
{
    public interface ICountryBLL
    {
        List<CountryVM> Get();
    }
}