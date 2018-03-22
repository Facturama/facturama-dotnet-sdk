using Newtonsoft.Json;

namespace Facturama.Models.Request
{
    public class Tax
    {
        [JsonProperty("Total")]
        public decimal Total { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Base")]
        public decimal Base { get; set; }

        [JsonProperty("Rate")]
        public decimal Rate { get; set; }

        [JsonProperty("IsRetention")]
        public bool IsRetention { get; set; }

        [JsonProperty("IsQuota")]
        public bool IsQuota { get; set; }
    }
}