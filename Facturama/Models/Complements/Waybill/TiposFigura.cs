using System;
using System.Collections.Generic;
using System.Text;

namespace Facturama.Models.Complements.Waybill
{
    public class TiposFigura
	{
        public PartesTransporte[] PartesTransporte { get; set; }
        public string TipoFigura { get; set; }
        public string RFCFigura { get; set; }
        
        public string NumLicencia { get; set; }
        
        public string NombreFigura { get; set; }
        
        public string NumRegIdTribFigura { get; set; }
        public string ResidenciaFiscalFigura { get; set; }

        public Domicilio Domicilio { get; set; }
    }
}
