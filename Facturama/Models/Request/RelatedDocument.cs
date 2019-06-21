using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Facturama.Models.Request
{
    class RelatedDocument
    {
        [JsonProperty("Uuid")]
        public String Uuid { get; set; }
        [JsonProperty("Serie")]
        public String Serie { get; set; }
        [JsonProperty("Folio")]
        public String Folio { get; set; }
        [JsonProperty("Currency")]
        public String Currency { get; set; }
        [JsonProperty("ExchangeRate")]
        public Double ExchangeRate { get; set; }
        [JsonProperty("PaymentMethod")]
        public String PaymentMethod { get; set; }
        [JsonProperty("PartialityNumber")]
        public int PartialityNumber { get; set; }
        [JsonProperty("PreviousBalanceAmount")]
        public Double PreviousBalanceAmount { get; set; }
        [JsonProperty("AmountPaid")]
        public Double AmountPaid { get; set; }
    }
}
