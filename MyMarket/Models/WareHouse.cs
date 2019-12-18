using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyMarket.Models
{
    public class WareHouse
    {
        public long Id { get; set; }


        [Required]
        public int AmountWareHouse { get; set; }



        [Required]
        public decimal UnitPriceBuying { get; set; }



        [Required]
        public decimal MargenGain { get; set; }


        [Required]
        public decimal UnitPrice { get
            {
                return (UnitPriceBuying * MargenGain / 100)+ UnitPriceBuying;
            }
            
        }


        [Required]
        public DateTime CreationTime { get; set; }


        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }


        public long ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }


    }
}