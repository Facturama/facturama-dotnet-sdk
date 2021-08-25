using System;
using System.Collections.Generic;
using System.Text;

namespace Facturama.Models.Retentions.Complementos.ServiciosPlataformasTecnologicas
{
	public class ImpuestosTrasladadosdelServicio
	{
		/// <summary>
		/// Atributo requerido para señalar la base para el cálculo del
		/// impuesto, la determinación de la base se realiza de acuerdo
		/// con las disposiciones fiscales vigentes.No se permiten valores negativos.
		/// </summary>		
		public decimal Base { get; set; }

		/// <summary>
		/// Atributo requerido para señalar la clave del tipo de impuesto
		/// trasladado aplicable al servicio.	
		/// </summary>
		public string Impuesto { get; set; }

		///// <summary>
		///// Atributo requerido para señalar la clave del tipo de factor que
		///// se aplica a la base del impuesto.
		///// </summary>
		//[Required]
		//[RegularExpression("Tasa|Cuota")]
		//public string TipoFactor { get; set; }

		/// <summary>
		/// Atributo requerido para señalar el valor de la tasa o cuota del
		/// impuesto que se traslada para el presente servicio.
		/// </summary>
		public string TasaCuota { get; set; }

		/// <summary>
		/// Atributo requerido para señalar el importe del impuesto
		/// trasladado que aplica al servicio.No se permiten valores
		/// negativos.
		/// </summary>
		public decimal Importe { get; set; }
	}
}
