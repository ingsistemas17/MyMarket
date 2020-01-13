using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMarket.Controllers.Dto
{
    public class FindCustomerOutDto
    {

        [Required]
        public int IdentificationNumber { get; set; }

        [Required]
        public string FirstName { get; set; }
    }
}