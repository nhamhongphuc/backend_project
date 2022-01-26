using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public enum Ranking 
    { 
        bad, normal, excellent
    }
    public class Review
    {
        public int ProductID { get; set; }

        public string AccountID { get; set; }

        public Ranking Ranking { get; set; }

        [StringLength(80, ErrorMessage = "Không vượt quá 80 kí tự!")]
        public string Comment { get; set; }

        [DisplayFormat(DataFormatString = "0:dd/MM/yyyy", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }


        [ForeignKey("AccountID")]
        public Account Account { get; set; }
        [ForeignKey("ProductID")]
        public Product Product { get; set; }
    }
}
