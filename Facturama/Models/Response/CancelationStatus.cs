using Newtonsoft.Json;

namespace Facturama.Models.Response
{
	/// <summary>
	/// Modelo de respuesta de las llamadas de cancelación de la API
	/// </summary>
	public class CancelationStatus
    {		
		/// <summary>
		/// Estado del CFDI ( canceled, requested ,rejected )
		/// </summary>
		public string Status { get; set; }

		/// <summary>
		/// UUID del CFDI consultado
		/// </summary>
		public string Uuid { get; set; }


		/// <summary>
		/// Fecha de solicitud de la cancelación
		/// </summary>
		public string RequestDate { get; set; }

		/// <summary>
		/// Fecha en que se responde la solicitud de cancelación
		/// </summary>
		public string ResponseDate { get; set; }


		/// <summary>
		/// Vigencia de la solicitud de cancelación
		/// </summary>
		public string ExpirationDate { get; set; }


		/// <summary>
		/// Vigencia de la solicitud de cancelación
		/// </summary>
		public string AcuseXmlBase64 { get; set; }
	}
}
