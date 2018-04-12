using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturama.Models.Request
{
    public class CfdiMulti : Cfdi
    {
        public string Folio { get; set; }
        public Issuer Issuer { get; set; }
    }

    public class Issuer
    {
        public string FiscalRegime { get; set; }
        public string Rfc { get; set; }
        public string Name { get; set; }
    }
}
