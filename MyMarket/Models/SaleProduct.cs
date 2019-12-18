using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyMarket.Models
{
    public class SaleProduct
    {

        public long Id { get; set; }


        [Required]
        public int Amount { get; set; }

        [Required]
        public bool IsLoading { get; set; }


        [Required]
        public decimal UnitPrice { get; set; }



        [Required]
        public decimal TotalPrice { get; set; }



        [Required]
        public DateTime CreationTime { get; set; }



        public long ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }



        public long ReceiptId { get; set; }

        [ForeignKey("ReceiptId")]
        public virtual Receipt Receipt { get; set; }
    }
}