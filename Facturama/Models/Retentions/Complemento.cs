using Facturama.Models.Retentions.Complementos.ServiciosPlataformasTecnologicas;
using Facturama.Models.Retentions.Complementos.TimbreFiscal;

namespace Facturama.Models.Retentions
{
	public class Complemento
	{
		/// <summary>
		/// Complemento para expresar la información sobre los servicios prestados
		/// por personas físicas que utilicen plataformas tecnológicas.
		/// </summary>
		public ServiciosPlataformasTecnologicas ServiciosPlataformasTecnologicas { get; set; }

		/// <summary>
		/// Timbre Fiscal
		/// </summary>
		public TimbreFiscalDigital TimbreFiscalDigital { get; set; }
	}
}
