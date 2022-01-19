using Newtonsoft.Json;

namespace Facturama.Models.Complements.Waybill
{
    public class Domicilio
	{
        [JsonProperty("Calle")]
        public string Calle { get; set; }

        [JsonProperty("NumeroExterior")]
        public string NumeroExterior { get; set; }

        [JsonProperty("NumeroInterior")]
        public string NumeroInterior { get; set; }

        [JsonProperty("Colonia")]
        public string Colonia { get; set; }

        [JsonProperty("Localidad")]
        public string Localidad { get; set; }

        [JsonProperty("Referencia")]
        public string Referencia { get; set; }

        [JsonProperty("Municipio")]
        public string Municipio { get; set; }

        [JsonProperty("Estado")]
        public string Estado { get; set; }

        [JsonProperty("Pais")]
        public string Pais { get; set; }

        [JsonProperty("CodigoPostal")]
        public string CodigoPostal { get; set; }
    }
}
