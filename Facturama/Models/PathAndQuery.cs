using System.Collections.Generic;

namespace Facturama.Models
{
    public class PathAndQuery
    {
        public string Path {  get; set; }
        public Dictionary<string, string> QueryParams { get; set; }
    }
}
