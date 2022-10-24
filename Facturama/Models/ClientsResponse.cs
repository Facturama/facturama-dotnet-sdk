
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Facturama.Models
{


    public class JsonResponse
    {
        [JsonProperty("recordsTotal")]
        public long RecordsTotal { get; set; }

        [JsonProperty("recordsFiltered")]
        public long RecordsFiltered { get; set; }

        [JsonProperty("data")]
        public object[] Data { get; set; }
    }
}
