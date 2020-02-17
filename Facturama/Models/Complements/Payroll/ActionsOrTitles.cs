using Newtonsoft.Json;

namespace Facturama.Models.Complements.Payroll
{
	public class ActionsOrTitles
	{
		[JsonProperty("MarketValue")]
		public decimal MarketValue { get; set; }

		[JsonProperty("PriceWhenGranting")]
		public decimal PriceWhenGranting { get; set; }
	}
}