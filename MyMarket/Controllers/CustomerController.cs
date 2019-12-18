using Microsoft.AspNet.Identity.EntityFramework;
using MyMarket.Controllers.Dto;
using MyMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyMarket.Controllers
{
    /// <summary>
    /// Controller for the Customer API endpoint
    /// </summary>
    [RoutePrefix("customer")]
    public class CustomerController : ApiController
    {

        /// <summary>
        /// Datastore access context
        /// </summary>
        private ExampleContext db;

        /// <summary>
        /// Handles initializing shared properties
        /// </summary>
        public CustomerController()
        {
            // initalize our access to th database
            db = new ExampleContext();
        }

        [Authorize]
        [Route("createcustomer")]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            IdentityUser user = db.Users.Where(a => a.UserName == ControllerContext.RequestContext.Principal.Identity.Name).FirstOrDefault();

            if (user == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("User doesn't exist."),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }

            if (customerDto == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("CustomerDto parameter doesn't exist."),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }

            var p = db.Customers.Where(a => a.IdentificationNumber == customerDto.IdentificationNumber).FirstOrDefault();

            if (p != null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("customer indentification already exist."),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }

            Customer customer = new Customer()
            {
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                IdentificationNumber = customerDto.IdentificationNumber,
                Mail = customerDto.Mail,
                Address = customerDto.Address,
                PhoneNumber = customerDto.PhoneNumber,
                UserId = user.Id,
                CreationTime = DateTime.Now

            };

            db.Customers.Add(customer);
            db.SaveChanges();

            return Ok("Customer was created succesfully");


        }
    }
}
