using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EntityFrameworkCoreDemo.DAL.IDAL;
using EntityFrameworkCoreDemo.EF;
using EntityFrameworkCoreDemo.Models.EntityModel;
using EntityFrameworkCoreDemo.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreDemo.DAL
{
    public class CountryDAL : ICountryDAL, IDisposable
    {
        private readonly DemoDbContext _dbContext;
        private readonly string        _currentLanguage;

        public CountryDAL(DemoDbContext dbContext)
        {
            _dbContext = dbContext;
            _currentLanguage = Thread.CurrentThread.CurrentUICulture.ToString();
        }

        public IEnumerable<Country> Get()
        {
            // 使用 Left Join
            return _dbContext.Country
                             .Include(c => c.CountryLanguages)
                             // .ThenInclude()
                             .AsNoTracking();
        }

        public bool Add(Country country)
        {
            throw new NotImplementedException();
        }

        public Country Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Country entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}