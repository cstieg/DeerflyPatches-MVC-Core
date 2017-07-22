using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeerflyPatches.Modules
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
    }
}
