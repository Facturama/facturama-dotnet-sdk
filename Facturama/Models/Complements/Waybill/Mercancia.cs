using Newtonsoft.Json;

namespace Facturama.Models.Complements.Waybill
{
    public class Mercancia
	{
        [JsonProperty("BienesTransp")]
        public string BienesTransp { get; set; }

        [JsonProperty("ClaveSTCC")]
        public string ClaveSTCC { get; set; }

        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }
        
        [JsonProperty("Cantidad")]
        public decimal Cantidad { get; set; }
        
        [JsonProperty("ClaveUnidad")]
        public string ClaveUnidad { get; set; }

        [JsonProperty("Unidad")]
        public string Unidad { get; set; }

        [JsonProperty("Dimensiones")]
        public string Dimensiones { get; set; }

        [JsonProperty("MaterialPeligroso")]
        public string MaterialPeligroso { get; set; }

        [JsonProperty("CveMaterialPeligroso")]
        public string CveMaterialPeligroso { get; set; }

        [JsonProperty("Embalaje")]
        public string Embalaje { get; set; }

        [JsonProperty("DescripEmbalaje")]
        public string DescripEmbalaje { get; set; }

        [JsonProperty("PesoEnKg")]
        public decimal PesoEnKg { get; set; }

        [JsonProperty("ValorMercancia")]
        public decimal ValorMercancia { get; set; }

        [JsonProperty("Moneda")]
        public string Moneda { get; set; }

        [JsonProperty("FraccionArancelaria")]
        public string FraccionArancelaria { get; set; }

        [JsonProperty("UUIDComercioExt")]
        public string UUIDComercioExt { get; set; }

        [JsonProperty("Pedimentos")]
        public Pedimentos[] Pedimentos { get; set; }

        [JsonProperty("GuiasIdentificacion")]
        public GuiasIdentificacion[] GuiasIdentificacion { get; set; }

        [JsonProperty("CantidadTransporta")]
        public CantidadTransporta[] CantidadTransporta { get; set; }

        [JsonProperty("DetalleMercancia")]
        public DetalleMercancia DetalleMercancia { get; set; }
    }
}
