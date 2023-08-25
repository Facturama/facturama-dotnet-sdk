using Newtonsoft.Json;
using System.Collections.Generic;

namespace Facturama.Models
{
    public class Product
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Unit")]
        public string Unit { get; set; }

        [JsonProperty("UnitCode")]
        public string UnitCode { get; set; }

        [JsonProperty("IdentificationNumber")]
        public string IdentificationNumber { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Price")]
        public decimal Price { get; set; }

        [JsonProperty("CodeProdServ")]
        public string CodeProdServ { get; set; }

        [JsonProperty("CuentaPredial")]
        public string CuentaPredial { get; set; }

        [JsonProperty("ObjetoImp")]
        public string ObjetoImp { get; set; }

        [JsonProperty("Taxes")]
        public IEnumerable<Request.Tax> Taxes { get; set; }
    }
}
