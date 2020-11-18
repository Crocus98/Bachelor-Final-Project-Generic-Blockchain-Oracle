using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oracle888730.OracleEF.Models
{
    [Table("ServiceTypes")]
    class ServiceType
    {
        [Key]
        public int ServiceTypeId { get; set; }
        [Required]
        public string ServiceTypeString { get; set; }

        public int ServiceForeignKey { get; set; }

        public Service Service { get; set; }

        public List<Subscriber> Subscribers { get; set; }

    }
}
