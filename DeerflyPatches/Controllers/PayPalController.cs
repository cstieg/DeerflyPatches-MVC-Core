using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DeerflyPatches.ViewModels;
using DeerflyPatches.Modules;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using System.Net;

namespace DeerflyPatches.Controllers
{
    public class PayPalController : Controller
    {
        private IHostingEnvironment _env;

        public PayPalController(IHostingEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        public async Task<string> CreateOrder()
        {
            // Get shopping cart from session
            ShoppingCart shoppingCart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("_shopping_cart");

            PayPalApiClient paypalClient = new PayPalApiClient();

            // Get client id
            ClientInfo paypalSecrets = paypalClient.GetClientSecrets(_env.ContentRootPath);

            string client_id = paypalSecrets.ClientId;
            string client_secret = paypalSecrets.ClientSecret;

            var access_token = "";

            // Get access token
            // TODO: Store access token to reuse until expires
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en_US"));

                var byteArray = Encoding.ASCII.GetBytes(client_id + ":" + client_secret);
                var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Authorization = header;

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.sandbox.paypal.com/v1/oauth2/token");
                request.Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                });

                var response = await client.SendAsync(request);
                var result = response.Content.ReadAsStringAsync().Result;
                access_token = new JsonDeserializer(result).GetString("access_token");
            }



            // Create order object
            object data = new
            {
                intent = "order",
                payer = new {
                    payment_method = "paypal"
                },
                transactions = new List<object>
                {
                    new
                    {
                        amount = new
                        {
                            currency = "USD",
                            total = "10.00",
                            details = new
                            {
                                shipping = "0.00",
                                subtotal = "10.00",
                                tax = "0.00"
                            }
                        },
                        payee = new
                        {
                            email = "cstieg4899-facilitator@yahoo.com"
                        },
                        description = "Order from Detex, manufacturer of Deerfly Patches",
                        item_list = new
                        {
                            items = new List<Object>
                            {
                                new
                                {
                                    name = "DFP",
                                    quantity = "1",
                                    price = "10.00",
                                    sku = "1",
                                    currency = "USD"
                                }
                            },
                            shipping_address = new
                            {
                                recipient_name = "Christopher Stieg",
                                line1 = "17852 Ten Mile Road",
                                line2 = "",
                                city = "LeRoy",
                                country_code = "US",
                                postal_code = "49655",
                                phone = "2316800095",
                                state = "MI"
                            }
                        }
                    }
                },
                redirect_urls = new
                {
                    return_url = "http://localhost:50138/Home/ShoppingCart",
                    cancel_url = "http://localhost:50138/Home/ShoppingCart"
                }
            };
            string dataJSON = JsonConvert.SerializeObject(data);
            // Create order with PayPal

            string orderId = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.sandbox.paypal.com/v1/payments/payment");
                request.Content = new StringContent(dataJSON, Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);
                var result = response.Content.ReadAsStringAsync().Result;
                orderId = new JsonDeserializer(result).GetString("id");

            }


            return orderId;
        }


        [HttpPost]
        public void ExecutePayment()
        {

        }
        
    }



}