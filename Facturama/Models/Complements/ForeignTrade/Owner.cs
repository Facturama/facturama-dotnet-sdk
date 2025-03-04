using Newtonsoft.Json;

namespace Facturama.Models.Complements.ForeignTrade
{
    public class Owner
    {
        /// <summary>
        /// N�mero de registro de identificaci�n tributaria del propietario.
        /// </summary>
        [JsonProperty("NumRegIdTrib")]
        public string NumRegIdTrib { get; set; }

        /// <summary>
        /// Clave del pa�s de residencia fiscal del propietario.
        /// </summary>
        [JsonProperty("TaxResidence")]
        public string TaxResidence { get; set; }
    }
}