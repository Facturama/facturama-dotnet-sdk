using Newtonsoft.Json;

namespace Facturama.Models.Request
{
    public class Image
    {
        [JsonProperty("Image")]
        public string Img { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }
    }
}
