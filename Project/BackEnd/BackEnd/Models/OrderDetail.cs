using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class OrderDetail
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Capacity { get; set; }
        public double Money { get; set; }


        [ForeignKey("ProductID")]
        public Product Product { get; set; }
        [ForeignKey("OrderID")]
        public Order Order { get; set; }
    }
}
