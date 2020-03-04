using Newtonsoft.Json;

namespace Facturama.Models.Complements.Payroll
{
	public class EntitySncf
	{
		[JsonProperty("OriginSource")]
		public string OriginSource { get; set; }

		[JsonProperty("AmountOriginSource")]
		public decimal? AmountOriginSource { get; set; }
	}
}