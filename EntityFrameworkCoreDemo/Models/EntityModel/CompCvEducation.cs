using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCoreDemo.Models.EntityModel
{
    [Table("CompCvEducation")]
    public class CompCvEducation
    {
        [Key]
        public Guid EducationId { get; set; }

        public Guid CvId { get; set; }

        [MaxLength(50)]
        public string AcademyName { get; set; }

        [ForeignKey("CvId")]
        public CompCv CompCv { get; set; }
    }
}