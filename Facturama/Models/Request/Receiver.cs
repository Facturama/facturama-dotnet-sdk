using Newtonsoft.Json;

namespace Facturama.Models.Request
{
    public class Receiver
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Rfc")]
        public string Rfc { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("CfdiUse")]
        public string CfdiUse { get; set; }

        [JsonProperty("FiscalRegime")]
        public string FiscalRegime { get; set; }

        [JsonProperty("TaxZipCode")]
        public string TaxZipCode { get; set; }

        [JsonProperty("TaxResidence")]
        public string TaxResidence { get; set; }

        [JsonProperty("TaxRegistrationNumber")]
        public string TaxRegistrationNumber { get; set; }

        [JsonProperty("Address")]
        public Address Address { get; set; }
    }
}