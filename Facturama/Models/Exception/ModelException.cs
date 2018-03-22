using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Facturama.Models.Exception
{
    public class ModelException
    {
        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("ModelState")]
        public Dictionary<string, string[]> Details { get; set; }
        
    }
}
