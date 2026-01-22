using System;
using System.Collections.Generic;
using System.Text;

namespace Facturama.Models.Response
{
    public class UploadCsdResponse
    {
        public string SerialNumber { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public string Certificate { get; set; }
    }
}
