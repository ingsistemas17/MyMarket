using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyMarket.Models
{
    public class Receipt
    {
        public long Id { get; set; }

        [Required]
        public int CodeReceipt { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public decimal IVA { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }


        public long CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }




    }
}