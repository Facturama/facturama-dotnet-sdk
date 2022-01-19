using Newtonsoft.Json;

namespace Facturama.Models.Complements.Waybill
{
    public class GuiasIdentificacion
	{
        [JsonProperty("NumeroGuiaIdentificacion")]
        public string NumeroGuiaIdentificacion { get; set; }

        [JsonProperty("DescripGuiaIdentificacion")]
        public string DescripGuiaIdentificacion { get; set; }

        [JsonProperty("PesoGuiaIdentificacion")]
        public decimal PesoGuiaIdentificacion { get; set; }
    }
}
