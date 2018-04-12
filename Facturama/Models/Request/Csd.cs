using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturama.Models.Request
{
    public class Csd
    {
        public string Rfc { get; set; }
        public string Certificate { get; set; }
        public string PrivateKey { get; set; }
        public string PrivateKeyPassword { get; set; }
    }
}
