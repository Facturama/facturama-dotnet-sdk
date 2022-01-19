using Newtonsoft.Json;

namespace Facturama.Models.Complements.Waybill
{
    public class DetalleMercancia
	{
        [JsonProperty("UnidadPesoMerc")]
        public string UnidadPesoMerc { get; set; }

        [JsonProperty("PesoBruto")]
        public decimal PesoBruto { get; set; }

        [JsonProperty("PesoNeto")]
        public decimal PesoNeto { get; set; }

        [JsonProperty("PesoTara")]
        public decimal PesoTara { get; set; }

        [JsonProperty("NumPiezas")]
        public int NumPiezas { get; set; }
    }
}
