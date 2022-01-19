using Newtonsoft.Json;

namespace Facturama.Models.Complements.Waybill
{
    public class Mercancias
	{
        [JsonProperty("PesoBrutoTotal")]
        public decimal PesoBrutoTotal { get; set; }

        [JsonProperty("UnidadPeso")]
        public string UnidadPeso { get; set; }


        [JsonProperty("PesoNetoTotal")]
        public decimal PesoNetoTotal { get; set; }

        [JsonProperty("NumTotalMercancias")]
        public int NumTotalMercancias { get; set; }

        [JsonProperty("CargoPorTasacion")]
        public decimal CargoPorTasacion { get; set; }


        [JsonProperty("Mercancia")]
        public Mercancia[] Mercancia { get; set; }

        [JsonProperty("Autotransporte")]
        public Autotransporte Autotransporte { get; set; }

        [JsonProperty("TransporteMaritimo")]
        public TransporteMaritimo TransporteMaritimo { get; set; }

        [JsonProperty("TransporteAereo")]
        public TransporteAereo TransporteAereo { get; set; }

        [JsonProperty("TransporteFerroviario")]
        public TransporteFerroviario TransporteFerroviario { get; set; }

    }
}
