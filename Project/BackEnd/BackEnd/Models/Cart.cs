using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartID { get; set; }

        public string AccountID { get; set; }
        
        public int CartCapacity { get; set; }

        public double CartTotal { get; set; }



        [ForeignKey("AccountID")]
        public Account Account { get; set; }
        public ICollection<CartDetail> CartDetails { get; set; }
    }
}
