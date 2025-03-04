using System.Collections.Generic;
using Newtonsoft.Json;

namespace Facturama.Models.Complements.ForeignTrade
{
    public class Commodity
    {
        /// <summary>
        /// N�mero de parte, la clave de identificaci�n que asigna la empresa o el n�mero de serie de la mercanc�a exportada.
        /// </summary>
        [JsonProperty("IdentificationNumber")]
        public string IdentificationNumber { get; set; }

        /// <summary>
        /// Clave de la fracci�n arancelaria correspondiente a la descripci�n de la mercanc�a exportada.
        /// </summary>
        [JsonProperty("TariffFraction")]
        public string TariffFraction { get; set; }


        /// <summary>
        /// Cantidad de bienes en la aduana conforme a la UnidadAduana cuando en el nodo Comprobante:Conceptos:Concepto se hubiera registrado informaci�n comercial.
        /// </summary>
        [JsonProperty("CustomsQuantity")]
        public string CustomsQuantity   { get; set; }

        /// <summary>
        /// Clave de la unidad de medida aplicable para la cantidad expresada en la mercanc�a en la aduana.
        /// </summary>
        [JsonProperty("CustomsUnit")]
        public string CustomsUnit { get; set; }

        /// <summary>
        /// Valor o precio unitario del bien en la aduana.
        /// </summary>
        [JsonProperty("CustomsUnitValue")]
        public decimal? CustomsUnitValue { get; set; }

        /// <summary>
        /// valor total en d�lares (USD).
        /// </summary>
        [JsonProperty("ValueInDolar")]
        public decimal ValueInDolar { get; set; }

        /// <summary>
        /// lista de descripciones espec�ficas de la mercanc�a.
        /// </summary>
        [JsonProperty("SpecificDescriptions")]
        public List<SpecificDescriptions> SpecificDescriptions { get; set; }


    }
}