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
    /// Controller for the warehouse API endpoint
    /// </summary>
    [RoutePrefix("warehouse")]
    public class WareHouseController : ApiController
    {
        /// <summary>
        /// Datastore access context
        /// </summary>
        private ExampleContext db;

        /// <summary>
        /// Handles initializing shared properties
        /// </summary>
        public WareHouseController()
        {
            // initalize our access to th database
            db = new ExampleContext();
        }


        [Authorize]
        [Route("loadingwarehouse")]
        public IHttpActionResult LoadingWareHouse(WareHouseDto wareHouseDto)
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

            if (wareHouseDto == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("wareHouseDto Parameter doesn't exist."),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }

            var p = db.BuyingProducts.Where(a => a.IsLoading == false).ToList();

            if (p == null || p.Count == 0)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("there is not product to load."),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }

            loadingProduct(p, wareHouseDto.MargenGain, user.Id);

           

            return Ok("warehouse was loaded succesfully");


        }

        private void loadingProduct(List<BuyingProduct> products, decimal margenGain, string userId)
        {

            foreach(var a in products)
            {
                var whProduct = db.WareHouses.FirstOrDefault(c => c.ProductId == a.ProductId);

                if(whProduct == null)
                {
                    WareHouse wareHouse = new WareHouse()
                    {
                        AmountWareHouse = a.AmountBuying,
                        MargenGain = margenGain,
                        ProductId = a.ProductId,
                        UnitPriceBuying = a.UnitPriceBuying,
                        UserId = userId,
                        CreationTime = DateTime.Now

                    };
                    db.WareHouses.Add(wareHouse);

                }
                else
                {
                    whProduct.MargenGain = margenGain;
                    whProduct.AmountWareHouse += a.AmountBuying;
                    db.SaveChanges();

                    //closed the loading
                    a.IsLoading = true;
                }

                db.SaveChanges();
            }



            
        }

        /// <summary>
        /// Releases managed and unmanaged resources based on parameters
        /// </summary>
        /// <param name="disposing">
        /// True: release managed and unamaged resources,
        /// False: release only unmanaged resources
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose of our datastore access context
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
