using System;
using Newtonsoft.Json;
using Facturama.Services.BaseService;

namespace Facturama.Models.Complements.Waybill
{
    public class Ubicacion
	{
        [JsonProperty("Domicilio")]
        public Domicilio Domicilio { get; set; }

        [JsonProperty("TipoUbicacion")]
        public TipoUbicacion TipoUbicacion { get; set; }

        [JsonProperty("IDUbicacion")]
        public string IDUbicacion { get; set; }

        [JsonProperty("RFCRemitenteDestinatario")]
        public string RFCRemitenteDestinatario { get; set; }

        [JsonProperty("NombreRemitenteDestinatario")]
        public string NombreRemitenteDestinatario { get; set; }

        [JsonProperty("NumRegIdTrib")]
        public string NumRegIdTrib { get; set; }

        [JsonProperty("ResidenciaFiscal")]
        public string ResidenciaFiscal { get; set; }

        [JsonProperty("NumEstacion")]
        public string NumEstacion { get; set; }

        [JsonProperty("NombreEstacion")]
        public string NombreEstacion { get; set; }

        [JsonProperty("NavegacionTrafico")]
        public string NavegacionTrafico { get; set; }

        [JsonProperty("FechaHoraSalidaLlegada")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-ddTHH:mm:ss")]
        public DateTime FechaHoraSalidaLlegada { get; set; }

        [JsonProperty("TipoEstacion")]
        public string TipoEstacion { get; set; }

        [JsonProperty("DistanciaRecorrida")]
        public decimal DistanciaRecorrida { get; set; }
    }
}
