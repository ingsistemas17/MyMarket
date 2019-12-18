using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMarket.Controllers.Dto
{
    public class ReceiptDto
    {

        public int CodeReceipt { get; set; }


        public decimal TotalPriceReceipt { get; set; }


        public int IdentificationNumber { get; set; }

        public decimal IVA { get; set; }


        public List<SaleProductOutDto> Products { get; set; }


    }
}