using Newtonsoft.Json;

namespace Facturama.Models.Complements.Waybill
{
    public class Autotransporte
	{
        [JsonProperty("PermSCT")]
        public string PermSCT { get; set; }

        [JsonProperty("NumPermisoSCT")]
        public string NumPermisoSCT { get; set; }

        [JsonProperty("Seguros")]
        public Seguros Seguros { get; set; }

        [JsonProperty("IdentificacionVehicular")]
        public IdentificacionVehicular IdentificacionVehicular { get; set; }

        [JsonProperty("Remolques")]
        public Remolque[] Remolques { get; set; }

    }
}
