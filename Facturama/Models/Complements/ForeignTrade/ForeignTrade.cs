using System.Collections.Generic;
using Newtonsoft.Json;

namespace Facturama.Models.Complements.ForeignTrade
{
    public class ForeignTrade
    {

        /// <summary>
        /// Emisor del complemento de comercio exterior
        /// </summary>
        [JsonProperty("Issuer")]
        public Issuer Issuer { get; set; }

        /// <summary>
        /// Receptor del complemento de comercio exterior
        /// </summary>
        [JsonProperty("Receiver")]
        public Receiver Receiver { get; set; }
        

        /// <summary>
        /// Datos del o los propietarios de la mecancía que se tarslada
        /// </summary>
        [JsonProperty("Owner")]
        public List<Owner> Owner { get; set; }

        /// <summary>
        /// Destinatario, datos complementarios del destinatario.
        /// </summary>
        [JsonProperty("Recipient")]
        public List<Recipient> Recipient { get; set; }

        /// <summary>
        /// Motivo Traslado
        /// </summary>
        [JsonProperty("ReasonForTrasnfer")]
        public string ReasonForTrasnfer { get; set; }


        /// <summary>
        /// Informacion de las mercancias que se trasladan
        /// </summary>
        [JsonProperty("Commodity")]
        public List<Commodity> Commodity { get; set; }


        /// <summary>
        /// clave de pedimento que se haya declarado conforme al catálogo
        /// </summary>
        [JsonProperty("RequestCode")]
        public string RequestCode { get; set; }


        /// <summary>
        /// Excepción de certificados de Origen de los Tratados de Libre Comercio.
        [JsonProperty("OriginCertificate")]
        public bool? OriginCertificate { get; set; }


        /// <summary>
        /// Folio del certificado de origen o el folio fiscal del CFDI con el que se pagó la expedición del certificado de origen.
        /// </summary>
        [JsonProperty("OriginCertificateNumber")]
        public string OriginCertificateNumber { get; set; }


        /// <summary>
        /// Número Exportador Confiable.
        /// </summary>
        [JsonProperty("ReliableExporterNumber")]
        public string ReliableExporterNumber { get; set; }


        /// <summary>
        ///  clave del INCOTERM aplicable a la factura.
        /// </summary>
        [JsonProperty("Incoterm")]
        public string Incoterm { get; set; }

        /// <summary>
        /// Información adicional.
        /// </summary>
        [JsonProperty("Observations")]
        public string Observations { get; set; }

        /// <summary>
        /// Tipo de Cambio USD, valor en pesos mexicanos que equivalen a un dólar de Estados Unidos de América
        /// </summary>
        [JsonProperty("ExchangeRateUSD")]
        public decimal? ExchangeRateUSD { get; set; }


        /// <summary>
        /// Total USD, importe total en dolares (USD)
        /// </summary>
        [JsonProperty("TotalUSD")]
        public decimal? TotalUSD { get; set; }


        
        


    }
}