using Newtonsoft.Json;

namespace Facturama.Models.Complements.ForeignTrade
{
    public class Owner
    {
        /// <summary>
        /// Número de registro de identificación tributaria del propietario.
        /// </summary>
        [JsonProperty("NumRegIdTrib")]
        public string NumRegIdTrib { get; set; }

        /// <summary>
        /// Clave del país de residencia fiscal del propietario.
        /// </summary>
        [JsonProperty("TaxResidence")]
        public string TaxResidence { get; set; }
    }
}