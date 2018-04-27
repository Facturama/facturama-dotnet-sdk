using System.Collections.Generic;
using Newtonsoft.Json;

namespace Facturama.Models.Request
{
    public class CfdiRelations
    {
        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Cfdis")]
        public List<CfdiRelation> Cfdis { get; set; }
    }
}