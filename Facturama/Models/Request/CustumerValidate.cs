using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturama.Models.Request
{
    public class CustumerValidate
    {

        /// <summary>
        /// Rfc, consultado
        /// </summary>
        [JsonProperty("Rfc")]
        public string Rfc { get; set; }

        /// <summary>
        /// Name, Nombre Fiscal
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; }

        /// <summary>
        /// ZipCode, Codigo postal 
        /// </summary>
        [JsonProperty("ZipCode")]
        public string ZipCode { get; set; }

        /// <summary>
        /// FiscalRegime, Regimen Fiscal
        /// </summary>
        [JsonProperty("FiscalRegime")]
        public string FiscalRegime { get; set; }

    }
}
