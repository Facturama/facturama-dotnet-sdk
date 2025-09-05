using System;
using System.Collections.Generic;
using System.Threading;

namespace Facturama.Models.Request
{
    public class HttpRequestOptions
    {
        public Dictionary<string, string> Headers { get; set; } 
        public Dictionary<string, string> QueryParams { get; set; }
        public string ContentType { get; set; } = "application/json";
        public TimeSpan? Timeout { get; set; }
        public CancellationToken CancellationToken { get; set; } = default;
    }
}
