using Newtonsoft.Json;


namespace Facturama.Models.Complements.ForeignTrade
{

    public class SpecificDescriptions
    {
        /// <summary>
        /// Marca de la Mercancia
        /// </summary>
        [JsonProperty("Brand")]
        public string Brand { get; set; }

        /// <summary>
        /// Modelo de la mercancía
        /// </summary>
        [JsonProperty("Model")]
        public string Model { get; set; }

        /// <summary>
        /// Submodelo de la mercancía
        /// </summary>
        [JsonProperty("Submodel")]
        public string SubModel { get; set; }

        /// <summary>
        /// Número de serie de la mercancía
        /// </summary>  
        [JsonProperty("SerialNumber")]
        public string SerialNumber { get; set; }


    }
}