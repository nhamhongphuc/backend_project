using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressID { get; set; }
        public string AccountID { get; set; } 
        
        [StringLength(60, ErrorMessage = "Không vượt quá 60 kí tự!")]
        public string Diachi { get; set; }

        [ForeignKey("AccountID")]
        public Account Account { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
