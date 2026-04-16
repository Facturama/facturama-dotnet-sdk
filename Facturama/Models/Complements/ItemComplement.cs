
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
        
        [JsonProperty("ThirdPartyAccount")]
        public ThirdPartyAccount.ThirdPartyAccount ThirdPartyAccount { get; set; }

        [JsonProperty("HidroYPetro")]
        public HidroYPetro.HidroYPetro HidroYPetro { get; set; }
    }
}


