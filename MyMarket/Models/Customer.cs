using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyMarket.Models
{
    public class Customer
    {
        public const int MaxNameLength = 50;
        public const int MaxAddressLength = 256;
        public const int MaxMailLength = 500;

        public long Id { get; set; }


        [Required]
        public int IdentificationNumber { get; set; }

        [Required]
        [StringLength(MaxNameLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(MaxNameLength)]
        public string LastName { get; set; }

        [StringLength(MaxAddressLength)]
        public string Address { get; set; }


        [StringLength(MaxMailLength)]
        public string Mail { get; set; }


        public int PhoneNumber { get; set; }


        [Required]
        public DateTime CreationTime { get; set; }


        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }
    }
}