using System;

namespace EntityFrameworkCoreDemo.Models.ViewModel
{
    public class CountyVM
    {
        public Guid   Id   { get; set; }

        // Country
        public Guid CountryId { get; set; }
        public string Language { get; set; }
        public string CountryName     { get; set; }

        // Language
        public Guid?  LanguageId { get; set; }
        public string Name       { get; set; }
    }
}