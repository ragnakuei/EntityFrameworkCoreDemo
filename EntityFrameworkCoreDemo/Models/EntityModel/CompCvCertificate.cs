using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCoreDemo.Models.EntityModel
{
    [Table("CompCvCertificate")]
    public class CompCvCertificate
    {
        [Key]
        public Guid CertificateId { get; set; }
        public Guid CvId { get;          set; }
        [MaxLength(50)]
        public string CertificateName { get; set; }
        [MaxLength(50)]
        public string CertificateIssuingUnit { get;    set; }
        public DateTime? CertificateIssuingDate { get; set; }
        [MaxLength(50)]
        public string Notes { get; set; }

        [ForeignKey("CvId")]
        public CompCv CompCv { get; set; }
    }
}