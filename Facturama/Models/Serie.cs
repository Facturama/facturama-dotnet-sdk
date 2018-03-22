using Newtonsoft.Json;

namespace Facturama.Models
{
    public class Serie
    {
        [JsonProperty("IdBranchOffice")]
        public string IdBranchOffice { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Folio")]
        public long Folio { get; set; }
    }
}
