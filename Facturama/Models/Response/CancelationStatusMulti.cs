using Newtonsoft.Json;

namespace Facturama.Models.Response
{
	/// <summary>
	/// Modelo de respuesta de las llamadas de cancelación de la API Multiemisor
	/// </summary>
	public class CancelationStatusMulti
	{
		/// <summary>
		/// Estado en que se encuntra la factura (canceled | active | pending)
		/// </summary>
		public string Status { get; set; }
		/// <summary>
		/// Mensaje descriptivo al status
		/// </summary>
		public string Message { get; set; }
		/// <summary>
		/// Identificador del CFDI
		/// </summary>
		public string Uuid { get; set; }
		/// <summary>
		/// Fecha en que se solicitó la cancelacion
		/// </summary>
		public string RequestDate { get; set; }
		/// <summary>
		/// Acuse XML en base64 en caso de existir
		/// </summary>
		public string AcuseXmlBase64 { get; set; }
		/// <summary>
		/// Fecha en que se canceló el CFDI
		/// </summary>
		public string CancelationDate { get; set; }
	}
}
