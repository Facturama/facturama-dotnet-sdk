using System.Collections.Generic;
using Microsoft.Win32;
using System.Security.Cryptography;
using Newtonsoft.Json;


namespace Facturama.Models.Complements.ForeignTrade
{
    public class Recipient
    {
        /// <summary>
        ///  Número de identificación o registro fiscal del país de residencia para efectos fiscales del destinatario de la mercancía exportada.
        /// </summary>
        [JsonProperty("NumRegIdTrib")]
        public string NumRegIdTrib { get; set; }

        /// <summary>
        /// Nombre completo, denominación o razón social del destinatario de la mercancía exportada.
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Address")]
        public List<Address> Addresses { get; set; }

    }
}