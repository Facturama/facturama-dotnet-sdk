using System;
using Facturama.Services;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Facturama.Models.Complements.Waybill
{
    public enum RegistroISTMO
    {
        [EnumMember(Value = "Sí")]
        Sí
    }

    public class ComplementoCartaPorte30
    {
        [JsonProperty("IdCCP")]
        public string IdCCP { get; set; }

        [JsonProperty("TranspInternac")]
        public TranspInternac TranspInternac { get; set; }

        [JsonProperty("RegimenAduanero")]
        public string RegimenAduanero { get; set; }

        [JsonProperty("EntradaSalidaMerc")]
        public string EntradaSalidaMerc { get; set; }

        [JsonProperty("PaisOrigenDestino")]
        public string PaisOrigenDestino { get; set; }

        [JsonProperty("ViaEntradaSalida")]
        public string ViaEntradaSalida { get; set; }

        [JsonProperty("TotalDistRec")]
        public decimal? TotalDistRec { get; set; }

        [JsonProperty("RegistroISTMO")]
        public RegistroISTMO RegistroISTMO { get; set; }

        [JsonProperty("UbicacionPoloOrigen")]
        public string UbicacionPoloOrigen { get; set; }

        [JsonProperty("UbicacionPoloDestino")]
        public string UbicacionPoloDestino { get; set; }

        [JsonProperty("Ubicaciones")]
        public Ubicacion[] Ubicaciones { get; set; }

        [JsonProperty("Mercancias")]
        public Mercancias Mercancias { get; set; }

        [JsonProperty("FiguraTransporte")]
        public TiposFigura[] FiguraTransporte { get; set; }


    }
}

