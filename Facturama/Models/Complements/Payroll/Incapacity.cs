using Newtonsoft.Json;

namespace Facturama.Models.Complements.Payroll
{
	public class Incapacity
	{
		[JsonProperty("Days")]
		public int Days { get; set; }

		[JsonProperty("Type")]
		public string Type { get; set; }

		[JsonProperty("Amount")]
		public decimal Amount { get; set; }
	}
}