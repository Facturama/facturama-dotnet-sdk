using Newtonsoft.Json;

namespace Facturama.Models.Complements.TaxLegends
{
    public class TaxLegends
    {
        [JsonProperty("Legends")]
        public Legend[] Legends { get; set; }

    }
    public class Legend
    {
        [JsonProperty("TaxProvision")]
        public string TaxProvision { get; set; }

        [JsonProperty("Norm")]
        public string Norm { get; set; }

        [JsonProperty("Text")]
        public string Text { get; set; }
    }
}


