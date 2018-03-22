using Newtonsoft.Json;

namespace Facturama.Models.Request
{
    public class TaxEntity
    {
        [JsonProperty("FiscalRegime")]
        public string FiscalRegime { get; set; }

        [JsonProperty("ComercialName")]
        public string ComercialName { get; set; }

        [JsonProperty("Rfc")]
        public string Rfc { get; set; }

        [JsonProperty("TaxName")]
        public string TaxName { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("TaxAddress")]
        public Address TaxAddress { get; set; }

        [JsonProperty("PasswordSat")]
        public string PasswordSat { get; set; }
    }
}
