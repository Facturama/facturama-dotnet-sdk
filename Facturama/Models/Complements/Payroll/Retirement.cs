using Newtonsoft.Json;

namespace Facturama.Models.Complements.Payroll
{
	public class Retirement
	{
		[JsonProperty("TotalASinglePayment")]
		public decimal TotalASinglePayment { get; set; }

		[JsonProperty("TotalParciality")]
		public decimal TotalParciality { get; set; }

		[JsonProperty("DailyAmount")]
		public decimal DailyAmount { get; set; }

		[JsonProperty("AccumulatedIncome")]
		public decimal AccumulatedIncome { get; set; }

		[JsonProperty("NonAccumulatedIncome")]
		public decimal NonAccumulatedIncome { get; set; }
	}
}