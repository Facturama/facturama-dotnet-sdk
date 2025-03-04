using System.Collections.Generic;
using Microsoft.Win32;
using System.Security.Cryptography;
using Newtonsoft.Json;


namespace Facturama.Models.Complements.ForeignTrade
{
    public class Recipient
    {
        /// <summary>
        ///  N�mero de identificaci�n o registro fiscal del pa�s de residencia para efectos fiscales del destinatario de la mercanc�a exportada.
        /// </summary>
        [JsonProperty("NumRegIdTrib")]
        public string NumRegIdTrib { get; set; }

        /// <summary>
        /// Nombre completo, denominaci�n o raz�n social del destinatario de la mercanc�a exportada.
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Address")]
        public List<Address> Addresses { get; set; }

    }
}