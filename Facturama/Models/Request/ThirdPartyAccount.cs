using Newtonsoft.Json;

namespace Facturama.Models.Request
{
    public class ThirdPartyAccount
    {
        [JsonProperty("Rfc")]
        public string Rfc { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("FiscalRegime")]
        public string FiscalRegime { get; set; }

        [JsonProperty("TaxZipCode")]
        public string TaxZipCode { get; set; }


    }
}
