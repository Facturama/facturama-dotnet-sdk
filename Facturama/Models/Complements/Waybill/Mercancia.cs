using Facturama.Services;
using Newtonsoft.Json;
using System;

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

        [JsonProperty("SectorCOFEPRIS")]
        public string SectorCOFEPRIS { get; set; }

        [JsonProperty("NombreIngredienteActivo")]
        public string NombreIngredienteActivo { get; set; }

        [JsonProperty("NomQuimico")]
        public string NomQuimico { get; set; }

        [JsonProperty("DenominacionGenericaProd")]
        public string DenominacionGenericaProd { get; set; }

        [JsonProperty("DenominacionDistintivaProd")]
        public string DenominacionDistintivaProd { get; set; }

        [JsonProperty("Fabricante")]
        public string Fabricante { get; set; }

        [JsonProperty("FechaCaducidad")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-ddTHH:mm:ss")]
        public DateTime? FechaCaducidad { get; set; }

        [JsonProperty("LoteMedicamento")]
        public string LoteMedicamento { get; set; }

        [JsonProperty("FormaFarmaceutica")]
        public string FormaFarmaceutica { get; set; }

        [JsonProperty("CondicionesEspTransp")]
        public string CondicionesEspTransp { get; set; }

        [JsonProperty("RegistroSanitarioFolioAutorizacion")]
        public string RegistroSanitarioFolioAutorizacion { get; set; }

        [JsonProperty("PermisoImportacion")]
        public string PermisoImportacion { get; set; }

        [JsonProperty("FolioImpoVUCEM")]
        public string FolioImpoVUCEM { get; set; }

        [JsonProperty("NumCAS")]
        public string NumCas { get; set; }

        [JsonProperty("RazonSocialEmpImp")]
        public string RazonSocialEmpImp { get; set; }

        [JsonProperty("NumRegSanPlagCOFEPRIS")]
        public string NumRegSanPlagCOFEPRIS { get; set; }

        [JsonProperty("DatosFabricante")]
        public string DatosFabricante { get; set; }

        [JsonProperty("DatosFormulador")]
        public string DatosFormulador { get; set; }

        [JsonProperty("DatosMaquilador")]
        public string DatosMaquilador { get; set; }

        [JsonProperty("UsoAutorizado")]
        public string UsoAutorizado { get; set; }


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
        [JsonProperty("TipoMaterial")]
        public string TipoMaterial { get; set; }

        [JsonProperty("DescripcionMaterial")]
        public string DescripcionMaterial { get; set; }

        [JsonProperty("DocumentacionAduanera")]
        public DocumentacionAduanera[] DocumentacionAduanera { get; set; }


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
