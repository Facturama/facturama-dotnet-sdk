
using System;

namespace Facturama.Models.Complements.Waybill
{
	public class Contenedor
	{
		public string MatriculaContenedor { get; set; }		
		public string TipoContenedor { get; set; }
		public string NumPrecinto { get; set; }
        public string IdCCPRelacionado { get; set; }
        public string PlacaVMCCP { get; set; }
        public DateTime FechaCertificacionCCP { get; set; }
    }
}
