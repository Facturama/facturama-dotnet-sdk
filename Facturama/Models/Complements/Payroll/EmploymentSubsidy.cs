using Newtonsoft.Json;

namespace Facturama.Models.Complements.Payroll
{
	public class EmploymentSubsidy
	{
		[JsonProperty("Amount")]
		public decimal Amount { get; set; }
	}
}