using Newtonsoft.Json;

namespace Facturama.Models.Complements.Waybill
{
	public class Remolque
	{
		[JsonProperty("SubTipoRem")]
		public string SubTipoRem { get; set; }


		[JsonProperty("Placa")]
		public string Placa { get; set; }
	}
}
