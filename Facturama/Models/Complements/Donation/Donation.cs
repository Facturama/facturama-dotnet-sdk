using Newtonsoft.Json;

namespace Facturama.Models.Complements.Donation
{
    public class Donation
    {
        /// <summary>
        /// Atributo requerido para expresar la fecha del oficio en que 
        /// se haya informado a la organización civil o fideicomiso, la 
        /// procedencia de la autorización para recibir donativos 
        /// deducibles, o su renovación correspondiente otorgada por 
        /// el Servicio de Administración Tributaria.
        /// </summary>
        [JsonProperty("AuthorizationDate")]
        public string AuthorizationDate { get; set; }

        /// <summary>
        /// Atributo requerido para expresar el número del oficio en que se haya informado 
        /// a la organización civil o fideicomiso, 
        /// la procedencia de la autorización para recibir donativos deducibles, 
        /// o su renovación correspondiente otorgada por el Servicio de Administración Tributaria.
        /// </summary>
        [JsonProperty("AuthorizationNumber")]
        public string AuthorizationNumber { get; set; }

        /// <summary>
        /// Atributo requerido para señalar de manera expresa que el
        /// comprobante que se expide se deriva de un donativo.
        /// </summary>
        [JsonProperty("Legend")]
        public string Legend { get; set; }
    }
}
