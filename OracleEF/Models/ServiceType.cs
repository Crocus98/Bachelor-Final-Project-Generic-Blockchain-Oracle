using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oracle888730.OracleEF.Models
{
    [Table("ServiceTypes")]
    public class ServiceType
    {
        [Key]
        public int ServiceTypeId { get; set; }
        [Required]
        public string ServiceTypeString { get; set; }
        [Required]
        public int ServiceForeignKey { get; set; }

        public Service Service { get; set; }

        public List<Subscriber> Subscribers { get; set; }

    }
}
