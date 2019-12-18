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
    /// Controller for the buyingproduct API endpoint
    /// </summary>
    [RoutePrefix("buyingproduct")]
    public class BuyingProductController : ApiController
    {
        /// <summary>
        /// Datastore access context
        /// </summary>
        private ExampleContext db;

        /// <summary>
        /// Handles initializing shared properties
        /// </summary>
        public BuyingProductController()
        {
            // initalize our access to th database
            db = new ExampleContext();
        }

        [Authorize]
        [Route("buyproduct")]
        public IHttpActionResult CreateCustomer(BuyingProductDto buyingProductDto)
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

            if (buyingProductDto == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("buyingProductDto parameter doesn't exist."),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }

            var p = db.Products.Where(a => a.Code == buyingProductDto.Code).FirstOrDefault();

            if (p == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("product code doesn't exist."),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }

            BuyingProduct buyingProduct = new BuyingProduct()
            {
                NumberBuying = buyingProductDto.NumberBuying,
                AmountBuying = buyingProductDto.AmountBuying,
                ProductId = p.Id,
                UnitPriceBuying = buyingProductDto.UnitPriceBuying,
                IsLoading = false,
                UserId = user.Id,
                CreationTime = DateTime.Now

            };

            db.BuyingProducts.Add(buyingProduct);
            db.SaveChanges();

            return Ok("buyingProduct was created succesfully");


        }
    }
}
