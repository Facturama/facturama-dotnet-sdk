using Newtonsoft.Json;

namespace Facturama.Models.Complements.Payroll
{
	public class Indemnification
	{
		[JsonProperty("TotalPaid")]
		public decimal TotalPaid { get; set; }

		[JsonProperty("YearsOfService")]
		public int YearsOfService { get; set; }

		[JsonProperty("LastMonthlySalaryOrd")]
		public decimal LastMonthlySalaryOrd { get; set; }

		[JsonProperty("AccumulatedIncome")]
		public decimal AccumulatedIncome { get; set; }

		[JsonProperty("NonAccumulatedIncome")]
		public decimal NonAccumulatedIncome { get; set; }
	}
}