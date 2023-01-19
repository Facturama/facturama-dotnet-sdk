using Newtonsoft.Json;

namespace Facturama.Models.Request
{
    public class GlobalInformation
    {
        [JsonProperty("Periodicity")]
        public string Periodicity { get; set; }

        [JsonProperty("Months")]
        public string Months { get; set; }
        
        [JsonProperty("Year")]
        public string Year { get; set; }
    }
}
