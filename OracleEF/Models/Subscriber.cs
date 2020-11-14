using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oracle888730.OracleEF.Models
{
    [Table("Subscribers")]
    class Subscriber
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int RequestType{ get; set; }
    }
}
