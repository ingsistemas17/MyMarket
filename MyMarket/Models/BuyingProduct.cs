using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyMarket.Models
{
    public class BuyingProduct
    {
        public long Id { get; set; }

        [Required]
        public int NumberBuying { get; set; }

        [Required]
        public decimal UnitPriceBuying { get; set; }


        [Required]
        public int AmountBuying { get; set; }

        [Required]
        public bool IsLoading { get; set; }


        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }

        [Required]
        public long ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }
    }
}