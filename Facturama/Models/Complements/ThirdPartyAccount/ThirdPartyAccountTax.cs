using Newtonsoft.Json;

namespace Facturama.Models.Complements.ThirdPartyAccount
{
	public class ThirdPartyAccountTax
	{
		[JsonProperty("Name")]
		public string Name { get; set; }

		[JsonProperty("Rate")]
		public decimal? Rate { get; set; }

		[JsonProperty("Amount")]
		public decimal Amount { get; set; }
	}
}