using System;
using System.Collections.Generic;
using System.Text;

namespace Facturama.Models.Retentions
{
	public class Receptor
	{
		/// <summary>
		/// Atributo requerido para expresar la nacionalidad del receptor del documento.
		/// </summary>		
		public string Nacionalidad { get; set; }

		/// <summary>
		/// Nodo requerido para expresar la información del contribuyente receptor en caso de que sea de nacionalidad mexicana.
		/// </summary>		
		public Nacional Nacional { get; set; }

		/// <summary>
		/// Nodo requerido para expresar la información del contribuyente receptor del documento cuando sea residente en el extranjero.
		/// </summary>		
		public Extranjero Extranjero { get; set; }
	}
}
