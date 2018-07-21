using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCoreDemo.Models.EntityModel
{
    [Table("CompCvLanguageRequirement")]
    public class CompCvLanguageRequirement
    {
        [Key]
        public Guid LanguageRequirement { get; set; }
        public Guid CvId { get; set; }
        public byte Language { get; set; }
        public byte Listening { get; set; }
        public byte Reading { get; set; }
        public byte Speaking { get; set; }
        public byte Writing { get; set; }

        [ForeignKey("CvId")]
        public CompCv CompCv { get; set; }
    }
}