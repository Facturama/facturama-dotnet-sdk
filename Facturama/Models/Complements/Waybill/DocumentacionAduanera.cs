using Facturama.Services;
using Newtonsoft.Json;
using System;

namespace Facturama.Models.Complements.Waybill
{
    public class DocumentacionAduanera
    {
        [JsonProperty("TipoDocumento")]
        public string TipoDocumento { get; set; }

        [JsonProperty("NumPedimento")]
        public string NumPedimento { get; set; }

        [JsonProperty("IdentDocAduanero")]
        public string IdentDocAduanero { get; set; }

        [JsonProperty("RFCImpo")]
        public string RFCImpo { get; set; }


    }
}
