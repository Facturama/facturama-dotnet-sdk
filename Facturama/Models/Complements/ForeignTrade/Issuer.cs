using Newtonsoft.Json;

namespace Facturama.Models.Complements.ForeignTrade
{
    public class Issuer
    {
        /// <summary>
        /// Direccion del emisor del complemento de comercio exterior.
        /// </summary>
        [JsonProperty("Address")]
        public Address Address { get; set; }

        /// <summary>
        /// Atributo condicional para expresar la CURPS del emisor del CFDI cuando es persona física.
        /// </summary>
        [JsonProperty("Curp")]
        public string Curp { get; set; }
    }
}
