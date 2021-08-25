using System;
using System.Collections.Generic;
using System.Text;

namespace Facturama.Models.Retentions
{
	public class Extranjero
	{
		/// <summary>
		/// Atributo opcional para expresar el número de registro de identificación fiscal del receptor del documento cuando
		///	sea residente en el extranjero.
		/// </summary>		
		public string NumRegIdTrib { get; set; }

		/// <summary>
		/// Atributo requerido para expresar el nombre, denominación o razón social del receptor del documento cuando sea
		/// residente en el extranjero.
		/// </summary>		
		public string NomDenRazSocR { get; set; }
	}
}
