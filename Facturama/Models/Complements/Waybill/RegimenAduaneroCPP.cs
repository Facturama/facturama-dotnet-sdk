using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturama.Models.Complements.Waybill
{
    public class RegimenAduaneroCPP
    {
        [JsonProperty("RegimenAduanero")]
        public string RegimenAduanero { get; set; }
    }
}
