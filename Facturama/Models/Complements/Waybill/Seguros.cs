using Newtonsoft.Json;

namespace Facturama.Models.Complements.Waybill
{
    public class Seguros
	{
        [JsonProperty("AseguraRespCivil")]
        public string AseguraRespCivil { get; set; }

        [JsonProperty("PolizaRespCivil")]
        public string PolizaRespCivil { get; set; }

        [JsonProperty("AseguraMedAmbiente")]
        public string AseguraMedAmbiente { get; set; }

        [JsonProperty("PolizaMedAmbiente")]
        public string PolizaMedAmbiente { get; set; }

        [JsonProperty("AseguraCarga")]
        public string AseguraCarga { get; set; }

        [JsonProperty("PolizaCarga")]
        public string PolizaCarga { get; set; }

        [JsonProperty("PrimaSeguro")]
        public decimal PrimaSeguro { get; set; }
    }
}
