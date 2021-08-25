using System;
using System.Collections.Generic;
using System.Text;

namespace Facturama.Models.Retentions.Complementos.TimbreFiscal
{
	public class TimbreFiscalDigital
	{
		public string Version { get; set; }
		public string Uuid { get; set; }
		public DateTime FechaTimbrado { get; set; }
		public string SelloCFD { get; set; }
		public string NoCertificadoSAT { get; set; }
		public string SelloSAT { get; set; }
	}
}
