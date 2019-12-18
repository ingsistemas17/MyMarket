using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMarket.Controllers.Dto
{
    public class CustomerDto
    {
        [Required]
        public int IdentificationNumber { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }


        public string Address { get; set; }



        public string Mail { get; set; }


        public int PhoneNumber { get; set; }
    }
}