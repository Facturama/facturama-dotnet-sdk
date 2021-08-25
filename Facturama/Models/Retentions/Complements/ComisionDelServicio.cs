using System;
using System.Collections.Generic;
using System.Text;

namespace Facturama.Models.Retentions.Complementos.ServiciosPlataformasTecnologicas
{
	public class ComisionDelServicio
	{
		/// <summary>
		/// Atributo opcional para registrar la base de la comisión del
		/// servicio de la plataforma, pagadas por personas físicas
		///	utilizando plataformas tecnológicas.
		/// </summary>
		public decimal? Base { get; set; }

		/// <summary>
		/// Atributo opcional para detallar el valor del porcentaje cobrado
		/// por la comisión del uso del servicio de las plataformas tecnológicas.
		/// </summary>
		public decimal? Porcentaje { get; set; }

		/// <summary>
		/// Atributo requerido para detallar el valor importe cobrado por
		/// la comisión del uso del servicio de las plataformas tecnológicas.
		/// </summary>
		public decimal Importe { get; set; }
	}
}
