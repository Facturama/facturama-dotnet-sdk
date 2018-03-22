using Newtonsoft.Json;

namespace Facturama.Models.Response
{
    public class Profile
    {
        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("ContactPhone")]
        public string ContactPhone { get; set; }

        [JsonProperty("HasRegistered")]
        public bool HasRegistered { get; set; }

        [JsonProperty("LoginProvider")]
        public string LoginProvider { get; set; }

        [JsonProperty("FiscalRegime")]
        public string FiscalRegime { get; set; }

        [JsonProperty("Rfc")]
        public string Rfc { get; set; }

        [JsonProperty("TaxName")]
        public string TaxName { get; set; }

        [JsonProperty("TaxAddress")]
        public Address TaxAddress { get; set; }
    }
}
