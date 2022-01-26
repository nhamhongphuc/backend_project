using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Account
    {
        [Key]

        [StringLength(40, ErrorMessage = "Không vượt quá 40 kí tự!")]
        public string AccountID { get; set; }

        [StringLength(20, ErrorMessage = "Không vượt quá 20 kí tự!")]
        public string AccountPassword { get; set; }

        public int UserID { get; set; }    

        public bool IsAdmin { get; set; }

        [DisplayFormat(DataFormatString = "0:dd/MM/yyyy", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }


        [ForeignKey("UserID")]
        public User User { get; set; }

        public Cart Cart { get; set; }

        public ICollection<SearchHistory> SearchHistorys { get; set; }
        
        public ICollection<Review> Reviews { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Address> Addresses { get; set; }        
    }
}
