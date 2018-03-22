using System;
using Newtonsoft.Json;

namespace Facturama.Models.Response
{
    class Suscription
    {
        [JsonProperty("Plan")]
        public string Plan { get; set; }

        [JsonProperty("RefInvoiceId")]
        public string RefInvoiceId { get; set; }

        [JsonProperty("CardId")]
        public string CardId { get; set; }

        [JsonProperty("CurrentFolios")]
        public string CurrentFolios { get; set; }

        [JsonProperty("CreationDate")]
        public DateTimeOffset CreationDate { get; set; }

        [JsonProperty("ExpirationDate")]
        public DateTimeOffset ExpirationDate { get; set; }

        [JsonProperty("Amount")]
        public double Amount { get; set; }
    }
}
