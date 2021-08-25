using System;
using System.Collections.Generic;
using System.Text;

namespace Facturama.Models.Retentions
{
	public class Periodo
	{
		/// <summary>
		/// Atributo requerido para la expresión del mes inicial del periodo de la retención e información de pagos.
		/// </summary>		
		public int MesIni { get; set; }

		/// <summary>
		/// Atributo requerido para la expresión del mes final del periodo de la retención e información de pagos.
		/// </summary>		
		public int MesFin { get; set; }

		/// <summary>
		/// Atributo requerido para la expresión del ejercicio fiscal (año).
		/// </summary>
		public int Ejerc { get; set; }
	}
}
