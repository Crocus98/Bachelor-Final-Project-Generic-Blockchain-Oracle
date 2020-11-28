using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oracle888730.OracleEF.Models
{
    [Table("Subscribers")]
    public class Subscriber
    {
        [Key]
        public int SubscriberId { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int ServiceTypeForeignKey { get; set; }

        public ServiceType ServiceType{ get; set; }

    }
}
