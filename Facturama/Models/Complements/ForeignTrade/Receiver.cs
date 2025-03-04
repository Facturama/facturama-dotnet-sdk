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
        ///  N�mero de identificaci�n o registro fiscal del pa�s de residencia para efectos fiscales del receptor del CFDI.
        /// </summary>
        [JsonProperty("NumRegIdTrib")]
        public string NumRegIdTrib { get; set; }


    }
}