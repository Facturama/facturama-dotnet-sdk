using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturama.Models.Response
{
    public class Csd : Request.Csd
    {
        public DateTime UploadDate { get; set; }
    }
}
