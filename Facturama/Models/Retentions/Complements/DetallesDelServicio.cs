using System;
using System.Collections.Generic;
using System.Text;

namespace Facturama.Models.Retentions.Complementos.ServiciosPlataformasTecnologicas
{
	public class DetallesDelServicio
	{
		/// <summary>
		/// Nodo requerido para detallar la información de los Impuestos
		/// trasladados de los servicios realizados por personas físicas utilizando
		///	plataformas tecnológicas.
		/// </summary>

		public ImpuestosTrasladadosdelServicio ImpuestosTrasladadosdelServicio { get; set; }

		/// <summary>
		/// Nodo opcional para detallar la información de las contribuciones
		/// gubernamentales pagadas por los servicios realizados por personas
		/// físicas utilizando plataformas tecnológicas.
		/// </summary>
		public ContribucionGubernamental ContribucionGubernamental { get; set; }

		/// <summary>
		/// Nodo requerido para detallar la información de la comisión pagada por el
		/// uso de plataformas tecnológicas por cada servicio relacionado.
		/// </summary>

		public ComisionDelServicio ComisionDelServicio { get; set; }

		/// <summary>
		/// Atributo requerido para expresar la clave de la forma de pago con la que se liquida el servicio.
		/// </summary>
		public string FormaPagoServ { get; set; }

		/// <summary>
		/// Atributo requerido para expresar la clave del tipo de servicio prestado.
		/// </summary>
		public string TipoDeServ { get; set; }

		/// <summary>
		/// Atributo condicional para identificar el subtipo del servicio prestado.
		/// </summary>
		public string SubTipServ { get; set; }

		/// <summary>
		/// Atributo opcional para registrar el RFC del tercero autorizado como personal de apoyo, por quien está registrado en la
		/// plataforma tecnológica para prestar servicios.	
		/// </summary>
		public string RfcTerceroAutorizado { get; set; }

		/// <summary>
		/// Atributo requerido para expresar la fecha en la que se prestó
		/// el servicio.
		/// </summary>
		public string FechaServ { get; set; }

		/// <summary>
		/// Atributo requerido para expresar el precio del servicio (sin
		/// incluir IVA).
		/// </summary>
		public decimal PrecioServSinIva { get; set; }
	}
}
