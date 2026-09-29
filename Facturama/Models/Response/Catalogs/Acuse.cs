using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Facturama.Models.Response.Catalogs
{
    public class Acuse
    {
        public string ContentEncoding { get; set; }
        public string  ContentType { get; set; }
        public string ContentLength { get; set; }
        public string Content { get; set; }
    }
}
