using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierID { get; set; }

        [StringLength(50, ErrorMessage = "Không vượt quá 50 kí tự!")]
        public string SupplierName { get; set; }

        [StringLength(50, ErrorMessage = "Không vượt quá 50 kí tự!")]
        public string SupplierAddress { get; set; }

        ICollection<Product> Products { get; set; }
    }
}
