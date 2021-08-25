
using Newtonsoft.Json;

namespace Facturama.Models.Complements
{
    /// <summary>
    /// Complementos del concepto
    /// </summary>
    public class ItemComplement
    {       
        [JsonProperty("EducationalInstitution")]
        public EducationalInstitution.EducationalInstitution EducationalInstitution { get; set; }
        
	}
}


