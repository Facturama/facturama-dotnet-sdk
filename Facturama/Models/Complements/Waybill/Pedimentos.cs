using Newtonsoft.Json;

namespace Facturama.Models.Complements.Waybill
{
	public class Pedimentos
	{
		[JsonProperty("Pedimento")]
		public string Pedimento { get; set; }
	}
}
