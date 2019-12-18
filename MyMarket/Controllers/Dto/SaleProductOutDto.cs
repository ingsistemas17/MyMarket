using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMarket.Controllers.Dto
{
    public class SaleProductOutDto
    {
        public int Amount { get;  set; }
        public long ProductId { get;  set; }
        public decimal UnitPrice { get;  set; }
        public decimal TotalPrice { get;  set; }
    }
}