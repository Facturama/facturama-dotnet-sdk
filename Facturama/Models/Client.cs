using Newtonsoft.Json;

namespace Facturama.Models
{
    public class Client
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("EmailOp1")]
        public string EmailOp1 { get; set; }

        [JsonProperty("EmailOp2")]
        public string EmailOp2 { get; set; }

        [JsonProperty("Address")]
        public Address Address { get; set; }

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

        [JsonProperty("NumRegIdTrib")]
        public string NumRegIdTrib { get; set; }
    }
}
