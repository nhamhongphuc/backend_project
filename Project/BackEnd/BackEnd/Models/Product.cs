using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        public int SupplierID { get; set; }

        public int ProductTypeID { get; set; }

        [StringLength(50, ErrorMessage = "Không vượt quá 50 kí tự!")]
        public string ProductName { get; set; }

        public double Price { get; set; }

        [StringLength(100, ErrorMessage = "Không vượt quá 100 kí tự!")]
        public string img_URL { get; set; }



        [ForeignKey("SupplierID")]
        public Supplier Supplier { get; set; }
        [ForeignKey("ProductTypeID")]
        public ProductType ProductType { get; set; }
        public ICollection<CartDetail> CartDetails { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
