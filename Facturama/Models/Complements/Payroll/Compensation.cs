using Newtonsoft.Json;

namespace Facturama.Models.Complements.Payroll
{
	public class Compensation
	{
		[JsonProperty("PositiveBalance")]
		public decimal PositiveBalance { get; set; }

		[JsonProperty("Year")]
		public short Year { get; set; }

		[JsonProperty("RemainingPositiveBalance")]
		public decimal RemainingPositiveBalance { get; set; }
	}
}