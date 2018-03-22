using Newtonsoft.Json;

namespace Facturama.Models
{
    public class Csd
    {
        [JsonProperty("Certificate")]
        public string Certificate { get; set; }

        [JsonProperty("PrivateKey")]
        public string PrivateKey { get; set; }

        [JsonProperty("PrivateKeyPassword")]
        public string PrivateKeyPassword { get; set; }
    }
}
