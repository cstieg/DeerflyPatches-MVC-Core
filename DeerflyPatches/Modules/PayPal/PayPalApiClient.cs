using DeerflyPatches.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DeerflyPatches.Modules.PayPal
{
    public class PayPalApiClient
    {
        public ClientInfo GetClientSecrets(string path)
        {
            string file = System.IO.Path.Combine(path, "PayPal.json");
            string json = System.IO.File.ReadAllText(file);
            ClientInfo paypalSecrets = JsonConvert.DeserializeObject<ClientInfo>(json);
            return paypalSecrets;
        }

        public async Task<AccessToken> GetAccessToken(ClientInfo paypalSecrets)
        {
            // TODO: Store access token to reuse until expires
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en_US"));

                var byteArray = Encoding.ASCII.GetBytes(paypalSecrets.ClientId + ":" + paypalSecrets.ClientSecret);
                var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Authorization = header;

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.sandbox.paypal.com/v1/oauth2/token");
                request.Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                });

                var response = await client.SendAsync(request);
                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<AccessToken>(result);
            }
        }

        public string CreateOrder(ShoppingCart shoppingCart)
        {
            object data = new
            {
                intent = "order",
                payer = new
                {
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
            return dataJSON;
        }

        public async Task<string> PostOrder(string data, string accessToken)
        {

            string orderId = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.sandbox.paypal.com/v1/payments/payment");
                request.Content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);
                var result = response.Content.ReadAsStringAsync().Result;
                orderId = new JsonDeserializer(result).GetString("id");
            }
            return orderId;
        }

    }
}
