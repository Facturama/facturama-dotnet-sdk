using Newtonsoft.Json;

namespace Facturama.Models.Complements.Payroll
{
	public class Outsourcing
	{
		[JsonProperty("RfcContractor")]
		public string RfcContractor { get; set; }

		[JsonProperty("PercentageTime")]
		public decimal PercentageTime { get; set; }
	}
}