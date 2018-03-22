using Newtonsoft.Json;

namespace Facturama.Models.Response.Catalogs
{
    public class CatalogViewModel
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }
    }
}