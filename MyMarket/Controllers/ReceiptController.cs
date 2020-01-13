using Microsoft.AspNet.Identity.EntityFramework;
using MyMarket.Controllers.Dto;
using MyMarket.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Http;

namespace MyMarket.Controllers
{
    /// <summary>
    /// Controller for the Products API endpoint
    /// </summary>
    [RoutePrefix("receipt")]
    public class ReceiptController : ApiController
    {
        /// <summary>
        /// Datastore access context
        /// </summary>
        private ExampleContext db;

        /// <summary>
        /// Handles initializing shared properties
        /// </summary>
        public ReceiptController()
        {
            // initalize our access to th database
            db = new ExampleContext();
        }


        [Authorize]
        [Route("salereceipt")]
        public IHttpActionResult SaleReceipt(ReceiptDto receiptDto)
        {
            IdentityUser user = db.Users.Where(a => a.UserName == ControllerContext.RequestContext.Principal.Identity.Name).FirstOrDefault();

            var pro = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SaleProductOutDto>>(receiptDto.Products);

            decimal totalPrice = pro.Sum(a => a.TotalPrice);

            if (user == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("User doesn't exist."),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }

            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {

                    saveReceipt(receiptDto, pro, totalPrice, user.Id);

                   

                    transaction.Commit();
                }
                catch(Exception e)
                {
                    transaction.Rollback();

                    throw e;
                }
            }

            return Ok("receipt was created succesfully");


        }

        private void saveReceipt(ReceiptDto receiptDto, List<SaleProductOutDto> products, decimal totalPrice, string userid)
        {
            Receipt receipt = new Receipt()
            {
                CodeReceipt = receiptDto.CodeReceipt,
                CustomerId = db.Customers.FirstOrDefault(a => a.IdentificationNumber == receiptDto.IdentificationNumber).Id,
                IVA = receiptDto.IVA,
                TotalPrice = (totalPrice * receiptDto.IVA / 100) + totalPrice,
                UserId = userid,
                CreationTime = DateTime.Now

            };

            db.Receipts.Add(receipt);

            foreach (var pro in products)
            {
                SaleProduct product = new SaleProduct()
                {
                    Amount = pro.Amount,
                    ProductId = pro.ProductId,
                    ReceiptId = receipt.Id,
                    UnitPrice = pro.UnitPrice,
                    TotalPrice = pro.TotalPrice,
                    IsLoading = true,
                    CreationTime = DateTime.Now

                };

                db.SaleProducts.Add(product);

                var wareHouse = db.WareHouses.FirstOrDefault(a => a.ProductId == pro.ProductId);

                if (wareHouse.AmountWareHouse - pro.Amount < 0)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("not enough units available  amount: " + wareHouse.AmountWareHouse),
                        StatusCode = HttpStatusCode.NotFound
                    };
                    throw new HttpResponseException(response);
                }

                wareHouse.AmountWareHouse -= pro.Amount;


            }

            db.SaveChanges();
        }

        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
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
