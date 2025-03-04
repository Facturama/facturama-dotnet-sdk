using Newtonsoft.Json;

namespace Facturama.Models.Complements.ForeignTrade
{
    public class AddressEmisor
    {
        [JsonProperty("Street")]
        public string Street { get; set; }

        [JsonProperty("ExteriorNumber")]
        public string ExteriorNumber { get; set; }

        [JsonProperty("InteriorNumber")]
        public string InteriorNumber { get; set; }

        [JsonProperty("Neighborhood")]
        public string Neighborhood { get; set; }

        [JsonProperty("Reference")]
        public string Reference { get; set; }

        [JsonProperty("Locality")]
        public string Locality { get; set; }

        [JsonProperty("Municipality")]
        public string Municipality { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("ZipCode")]
        public string ZipCode { get; set; }
    }
}