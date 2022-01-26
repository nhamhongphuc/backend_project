using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public enum Status
    {
        waitforconfirm, shipping, success, cancel
    }
    [Table("order_")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        public int AddressID { get; set; }
       
        public string AccountID { get; set; }

        [DisplayFormat(DataFormatString = "0:dd/MM/yyyy", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        //1. waitforconfirm   2. shipping  3. success  4. cancel
        [Column("Status_")]
        public int Status { get; set; }

        public double Total { get; set; }


        [ForeignKey("AccountID")]
        public Account Account { get; set; }
        [ForeignKey("AddressID")]
        public Address Address { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
