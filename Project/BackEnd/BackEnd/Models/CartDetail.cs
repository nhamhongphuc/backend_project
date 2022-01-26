using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class CartDetail
    {
        public int ProductID { get; set; }

        public int CartID { get; set; }

        public int Capacity { get; set; }

        public double Money { get; set; }

        [DisplayFormat(DataFormatString = "0:dd/MM/yyyy", ApplyFormatInEditMode = true)]
        public DateTime AddDate { get; set; }



        [ForeignKey("CartID")]
        public Cart Cart { get; set; }
        [ForeignKey("ProductID")]
        public Product Product { get; set; }
    }
}
