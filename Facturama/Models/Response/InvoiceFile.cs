using Newtonsoft.Json;

namespace Facturama.Models.Response
{
    public class InvoiceFile
    {
        [JsonProperty("ContentEncoding")]
        public string ContentEncoding { get; set; }

        [JsonProperty("ContentType")]
        public string ContentType { get; set; }

        [JsonProperty("ContentLength")]
        public long ContentLength { get; set; }

        [JsonProperty("Content")]
        public string Content { get; set; }
    }
}
