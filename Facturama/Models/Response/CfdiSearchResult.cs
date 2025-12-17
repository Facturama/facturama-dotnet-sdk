using Newtonsoft.Json;

namespace Facturama.Models.Response
{
    public class CfdiSearchResults
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("CfdiType")]
        public string CfdiType { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Folio")]
        public string Folio { get; set; }

        [JsonProperty("Serie")]
        public string Serie { get; set; }

        [JsonProperty("TaxName")]
        public string TaxName { get; set; }

        [JsonProperty("Rfc")]
        public string Rfc { get; set; }

        [JsonProperty("RfcIssuer")]
        public string RfcIssuer { get; set; }

        [JsonProperty("Date")]
        public System.DateTimeOffset Date { get; set; }

        [JsonProperty("Subtotal")]
        public decimal Subtotal { get; set; }

        [JsonProperty("Total")]
        public decimal Total { get; set; }

        [JsonProperty("Uuid")]
        public string Uuid { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("IsActive")]
        public bool IsActive { get; set; }

        [JsonProperty("PaymentMethod")]
        public string PaymentMethod { get; set; }

        [JsonProperty("OrderNumber")]
        public string OrderNumber { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("EmailSent")]
        public bool EmailSent { get; set; }
    }
}
