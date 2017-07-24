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
using DeerflyPatches.Modules.PayPal;
using DeerflyPatches.Models;

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
            if (shoppingCart.GetOrder().ShipTo == null)
            {
                shoppingCart.GetOrder().ShipTo = new Address();
            }
            shoppingCart.GetOrder().ShipTo.Country = "US";

            PayPalApiClient paypalClient = new PayPalApiClient();

            // Get client id from paypal.json
            ClientInfo paypalSecrets = paypalClient.GetClientSecrets(_env.ContentRootPath);

            // Get access token
            AccessToken accessToken = await paypalClient.GetAccessToken(paypalSecrets);

            // Create JSON string with order information
            shoppingCart.payeeEmail = paypalSecrets.ClientAccount;
            string orderData = paypalClient.CreateOrder(shoppingCart);

            // Post order to PayPal API and return order ID to front end
            return await paypalClient.PostOrder(orderData, accessToken.AccessTokenString);
        }


        [HttpPost]
        public async Task<IActionResult> ExecutePayment(string paymentId, IFormCollection data)
        {
            PayPalApiClient paypalClient = new PayPalApiClient();

            // Get client id from paypal.json
            ClientInfo paypalSecrets = paypalClient.GetClientSecrets(_env.ContentRootPath);

            // Get access token
            AccessToken accessToken = await paypalClient.GetAccessToken(paypalSecrets);

            string payerId = Request.Form["PayerID"];

            string uri = "https://api.sandbox.paypal.com/v1/payments/payment/" + paymentId + "/execute";
            string postData = JsonConvert.SerializeObject(new
            {
                payer_id = payerId
            });
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.AccessTokenString);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
                request.Content = new StringContent(postData, Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);

                var result = response.Content.ReadAsStringAsync().Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(result.ToString());
                }
                var orderId = new JsonDeserializer(result).GetString("id");
            }
            return Ok();
        }
    }



}