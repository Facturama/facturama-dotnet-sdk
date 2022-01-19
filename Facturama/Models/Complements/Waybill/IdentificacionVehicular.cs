using Newtonsoft.Json;

namespace Facturama.Models.Complements.Waybill
{
    public class IdentificacionVehicular
	{
        [JsonProperty("ConfigVehicular")]
        public string ConfigVehicular { get; set; }

        [JsonProperty("PlacaVM")]
        public string PlacaVM { get; set; }

        [JsonProperty("AnioModeloVM")]
        public int AnioModeloVM { get; set; }
    }
}
