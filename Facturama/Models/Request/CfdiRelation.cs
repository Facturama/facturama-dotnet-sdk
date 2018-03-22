using Newtonsoft.Json;

namespace Facturama.Models.Request
{
    public class CfdiRelation
    {

        [JsonProperty("Uuid")]
        public string Uuid { get; set; }
    }
}