using Microsoft.Win32;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace Facturama.Models.Complements.ForeignTrade
{
    public class Receiver
    {
        /// <summary>
        ///     
        /// </summary>
        [JsonProperty("Address")]
        public Address Address { get; set; }

        /// <summary>
        ///  Número de identificación o registro fiscal del país de residencia para efectos fiscales del receptor del CFDI.
        /// </summary>
        [JsonProperty("NumRegIdTrib")]
        public string NumRegIdTrib { get; set; }


    }
}