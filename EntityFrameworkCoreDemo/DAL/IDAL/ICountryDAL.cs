using System;
using System.Collections.Generic;
using EntityFrameworkCoreDemo.Models.EntityModel;

namespace EntityFrameworkCoreDemo.DAL.IDAL
{
    public interface ICountryDAL
    {
        IEnumerable<Country> Get();
        bool                 Add(Country    country);
        Country              Get(Guid       id);
        bool                 Update(Country entity);
        bool                 Delete(Guid    id);
    }
}