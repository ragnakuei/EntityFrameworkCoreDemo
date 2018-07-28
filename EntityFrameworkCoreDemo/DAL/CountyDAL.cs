﻿using System;
using System.Collections.Generic;
using System.Linq;
using EntityFrameworkCoreDemo.EF;
using EntityFrameworkCoreDemo.IDAL;
using EntityFrameworkCoreDemo.Models.EntityModel;
using EntityFrameworkCoreDemo.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCoreDemo.DAL
{
    public class CountyDAL : ICountyDAL, IDisposable
    {
        private readonly DemoDbContext      _dbContext;
        private readonly ILogger<CountyDAL> _logger;
        private readonly UserInfo           _userInfo;

        public CountyDAL(DemoDbContext      dbContext,
                         ILogger<CountyDAL> logger,
                         UserInfo           userInfo)
        {
            _dbContext = dbContext;
            _logger    = logger;
            _userInfo  = userInfo;
        }

        public IEnumerable<County> Get()
        {
            // 使用 Left Join
            return _dbContext.County
                             .Include(c => c.CountyLanguages)
                             .Include(c => c.Country)
                             .ThenInclude(c => c.CountryLanguages)
                             .AsNoTracking();
        }

        public County Get(Guid id)
        {
            // 分開查詢
            var county = _dbContext.County
                                   .AsNoTracking()
                                   .FirstOrDefault(c => c.CountyId == id);
            if (county == null)
                throw new Exception("查無資料");

            var countyLanguage = _dbContext.CountyLanguage
                                           .Where(l => l.CountyId    == id
                                                       && l.Language == _userInfo.CurrentLanguage)
                                           .AsNoTracking();
            county.CountyLanguages = countyLanguage.ToList();

            county.Country = _dbContext.Country
                                       .AsNoTracking()
                                       .First(c => c.CountryId == county.CountryId);
            return county;
        }

        public bool Add(County county)
        {
            _dbContext.County.Add(county);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Update(County updateEntity)
        {
            if (updateEntity == null)
                throw new Exception("County 無對應資料可更新");

            var countyInDB = _dbContext.County
                                       .First(c => c.CountyId == updateEntity.CountyId);
            if (countyInDB == null)
                throw new Exception("County 無對應資料可更新");

            _dbContext.County
                      .Update(updateEntity);

            _dbContext.CountyLanguage
                      .Update(updateEntity.CountyLanguages.First());

            // 同時更新二個 Table，會自動加上 transaction
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(Guid id)
        {
            var delCounty = _dbContext.County
                                      .FirstOrDefault(c => c.CountyId == id);

            var delCountyLanguages = _dbContext.CountyLanguage
                                               .Where(c => c.CountyId == delCounty.CountyId);

            _dbContext.CountyLanguage
                      .RemoveRange(delCountyLanguages);

            _dbContext.County
                      .Remove(delCounty);

            return _dbContext.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}