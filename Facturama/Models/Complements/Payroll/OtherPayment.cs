using Newtonsoft.Json;

namespace Facturama.Models.Complements.Payroll
{
	public class OtherPayment
	{
		[JsonProperty("EmploymentSubsidy")]
		public EmploymentSubsidy EmploymentSubsidy { get; set; }

		[JsonProperty("Compensation")]
		public Compensation Compensation { get; set; }

		[JsonProperty("OtherPaymentType")]
		public string OtherPaymentType { get; set; }

		[JsonProperty("Code")]
		public string Code { get; set; }

		[JsonProperty("Description")]
		public string Description { get; set; }

		[JsonProperty("Amount")]
		public decimal Amount { get; set; }
	}
}