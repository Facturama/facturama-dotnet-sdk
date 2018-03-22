using Newtonsoft.Json;

namespace Facturama.Models
{
    public class BranchOffice
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Address")]
        public Address Address { get; set; }
    }
}
