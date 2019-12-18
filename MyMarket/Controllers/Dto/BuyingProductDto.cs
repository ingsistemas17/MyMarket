using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMarket.Controllers.Dto
{
    public class BuyingProductDto
    {


        [Required]
        public int NumberBuying { get; set; }

        [Required]
        public decimal UnitPriceBuying { get; set; }


        [Required]
        public int AmountBuying { get; set; }

        [Required]
        public int Code { get; set; }

    }
}