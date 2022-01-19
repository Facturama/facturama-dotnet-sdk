using System;
using System.Collections.Generic;
using System.Text;

namespace Facturama.Models.Complements.Waybill
{
    public class TransporteAereo
	{
        public string PermSCT { get; set; }
        
        public string NumPermisoSCT { get; set; }
        

        public string MatriculaAeronave { get; set; }

        public string NombreAseg { get; set; }

        public string NumPolizaSeguro { get; set; }
        
        public string NumeroGuia { get; set; }
        public string LugarContrato { get; set; }
        
        public string CodigoTransportista { get; set; }
        public string NumRegIdTribTranspor { get; set; }
        public string ResidenciaFiscalTranspor { get; set; }
        public string NombreTransportista { get; set; }
        public string RFCEmbarcador { get; set; }
        public string NumRegIdTribEmbarc { get; set; }
        public string ResidenciaFiscalEmbarc { get; set; }
        public string NombreEmbarcador { get; set; }
    }
}
