
using Newtonsoft.Json;

namespace Facturama.Models.Complements.EducationalInstitution
{
	public class EducationalInstitution
	{
        /// <summary>
        /// Nombre del alumno
        /// </summary>
        [JsonProperty("StudentsName")]
        public string StudentsName { get; set; }

        /// <summary>
        /// Clave única de registro de población del alumno
        /// </summary>        
        [JsonProperty("Curp")]
        public string Curp { get; set; }

        /// <summary>
        /// Debe ser alguno de los siguientes:
        /// Preescolar|Primaria|Secundaria|Profesional técnico|Bachillerato o su equivalente
        /// </summary>
        [JsonProperty("EducationLevel")]
        public string EducationLevel { get; set; }

        /// <summary>
        /// Clave del centro de trabajo o el reconocimiento de validez oficial de esudios que tenga la instución educativa privada donde se realiza el pago
        /// </summary>
        [JsonProperty("AutRvoe")]
        public string AutRvoe { get; set; }

        /// <summary>
        /// RFC de quien realiza el pago cuando sea diferente a quien recibe el servicio (opcional)
        /// </summary>
        [JsonProperty("PaymentRfc")]
        public string PaymentRfc { get; set; }
    }
}
