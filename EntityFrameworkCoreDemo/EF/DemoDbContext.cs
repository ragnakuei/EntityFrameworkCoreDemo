using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFrameworkCoreDemo.Models.EntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCoreDemo.EF
{
    public class DemoDbContext : DbContext
    {
        //private readonly ILogger _logger;

        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {
            //_logger      = LogManager.GetCurrentClassLogger();
            //Database.Log = log => _logger.Debug(log);
        }

        //public DbSet<CompCv>                    CompCv                    { get; set; }
        //public DbSet<CompCvCertificate>         CompCvCertificate         { get; set; }
        //public DbSet<CompCvEducation>           CompCvEducation           { get; set; }
        //public DbSet<CompCvLanguageRequirement> CompCvLanguageRequirement { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<CountryLanguage> CountryLanguage { get; set; }
        //public DbSet<County>                    County                    { get; set; }

        //public DbSet<CountyLanguage> CountyLanguage { get; set; }
        //public DbSet<ActionReceipt>             ActionReceipt             { get; set; }
        //public DbSet<ActionReceiptNoteLanguage> ActionReceiptNoteLanguage { get; set; }
    }
}
