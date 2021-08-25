using System;
using System.Collections.Generic;
using System.Text;

namespace Facturama.Models.Retentions.Complementos.ServiciosPlataformasTecnologicas
{
	public class ContribucionGubernamental
	{
		/// <summary>
		/// Atributo requerido para registrar el importe de la contribución
		/// gubernamental pagada por los servicios realizados por
		///	personas físicas utilizando plataformas tecnológicas.
		/// </summary>
		public decimal ImpContrib { get; set; }

		/// <summary>
		/// Atributo requerido para registrar la clave de la entidad
		/// federativa donde se efectúa el pago de la contribución
		///	gubernamental.
		/// </summary>
		public string EntidadDondePagaLaContribucion { get; set; }
	}
}
