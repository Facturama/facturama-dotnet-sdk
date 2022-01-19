using System;
using Facturama.Services;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Facturama.Models.Complements.Waybill
{
    public enum TranspInternac
    {
        [EnumMember(Value = "Si")]
        Si,

        [EnumMember(Value = "No")]
        No,        
    }
    public enum TipoUbicacion
	{
        [EnumMember(Value = "Origen")]
        Origen,

        [EnumMember(Value = "Destino")]
        Destino,
    }

    public class ComplementoCartaPorte20
	{        
        [JsonProperty("TranspInternac")]
        public TranspInternac TranspInternac { get; set; }        

        [JsonProperty("EntradaSalidaMerc")]
        public string EntradaSalidaMerc { get; set; }

        [JsonProperty("PaisOrigenDestino")]
        public string PaisOrigenDestino { get; set; }

        [JsonProperty("ViaEntradaSalida")]
        public string ViaEntradaSalida { get; set; }

        [JsonProperty("TotalDistRec")]
        public decimal? TotalDistRec { get; set; }

        [JsonProperty("Ubicaciones")]
        public Ubicacion[] Ubicaciones { get; set; }

        [JsonProperty("Mercancias")]
        public Mercancias Mercancias { get; set; }
        
        [JsonProperty("FiguraTransporte")]
        public TiposFigura[] FiguraTransporte { get; set; }
    }
}
