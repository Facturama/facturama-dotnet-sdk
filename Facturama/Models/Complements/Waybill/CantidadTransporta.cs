using Newtonsoft.Json;

namespace Facturama.Models.Complements.Waybill
{
    public class CantidadTransporta
	{
        [JsonProperty("Cantidad")]
        public decimal Cantidad { get; set; }

        [JsonProperty("IDOrigen")]
        public string IDOrigen { get; set; }

        [JsonProperty("IDDestino")]
        public string IDDestino { get; set; }

        [JsonProperty("CvesTransporte")]
        public string CvesTransporte { get; set; }
    }
}
