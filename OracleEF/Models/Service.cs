using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oracle888730.OracleEF.Models
{

    [Table("Services")]
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        [Required]
        public string ServiceName { get; set; }

        public List<ServiceType> ServiceTypes { get; set; }
    }
}
