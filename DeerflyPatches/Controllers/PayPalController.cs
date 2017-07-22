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

            // Get client id from paypal.json
            ClientInfo paypalSecrets = paypalClient.GetClientSecrets(_env.ContentRootPath);

            // Get access token
            AccessToken accessToken = await paypalClient.GetAccessToken(paypalSecrets);

            // Create JSON string with order information
            string orderData = paypalClient.CreateOrder(shoppingCart);

            // Post order to PayPal API and return order ID to front end
            string orderID = await paypalClient.PostOrder(orderData, accessToken.AccessTokenString);
            return orderID;
        }


        [HttpPost]
        public void ExecutePayment()
        {

        }
        
    }



}