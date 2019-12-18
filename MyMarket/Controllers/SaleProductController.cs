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
    /// Controller for the Products API endpoint
    /// </summary>
    [RoutePrefix("saleproduct")]
    public class SaleProductController : ApiController
    {
        /// <summary>
        /// Datastore access context
        /// </summary>
        private ExampleContext db;

        /// <summary>
        /// Handles initializing shared properties
        /// </summary>
        public SaleProductController()
        {
            // initalize our access to th database
            db = new ExampleContext();
        }


        [Authorize]
        [HttpPost]
        [Route("getsaleproduct")]
        public SaleProductOutDto getSaleProduct([FromBody]SaleProductDto productDto)
        {
            WareHouse warehouse = null;

            if (productDto == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("productDto Parameter doesn't exist."),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }

            var p = db.Products.Where(a => a.Code == productDto.Code).FirstOrDefault();

            

            if (p == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("product code doesn't exist."),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }
            else
            {
                warehouse = db.WareHouses.Where(a => a.ProductId == p.Id).FirstOrDefault();


                if (warehouse == null)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("product code doesn't exist in the warehouse."),
                        StatusCode = HttpStatusCode.NotFound
                    };
                    throw new HttpResponseException(response);
                }else
                {
                    if(warehouse.AmountWareHouse - productDto.Amount < 0)
                    { 
                        var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent("not enough units available  amount: " + warehouse.AmountWareHouse),
                            StatusCode = HttpStatusCode.NotFound
                        };
                        throw new HttpResponseException(response);
                    }
                }
            }

            SaleProductOutDto product = new SaleProductOutDto()
            {
                Amount = productDto.Amount,
                ProductId = p.Id,
                UnitPrice = warehouse.UnitPrice,
                TotalPrice = warehouse.UnitPrice * productDto.Amount,
                
            };

            return product;

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
