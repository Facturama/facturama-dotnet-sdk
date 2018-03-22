using Facturama.Models.Response.Catalogs.Cfdi;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Facturama.Models.Request
{
    public class Item
    {
        [JsonProperty("ProductCode")]
        public string ProductCode { get; set; }

        [JsonProperty("IdentificationNumber")]
        public string IdentificationNumber { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Unit")]
        public string Unit { get; set; }

        [JsonProperty("UnitCode")]
        public string UnitCode { get; set; }

        [JsonProperty("UnitPrice")]
        public decimal UnitPrice { get; set; }

        [JsonProperty("Quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty("Subtotal")]
        public decimal Subtotal { get; set; }

        [JsonProperty("Discount")]
        public decimal Discount { get; set; }

        [JsonProperty("Taxes")]
        public List<Tax> Taxes { get; set; }

        [JsonProperty("CuentaPredial")]
        public string CuentaPredial { get; set; }

        [JsonProperty("Total")]
        public decimal Total { get; set; }
    }
}