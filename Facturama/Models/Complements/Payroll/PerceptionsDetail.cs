using Newtonsoft.Json;

namespace Facturama.Models.Complements.Payroll
{
	public class PerceptionsDetail
	{
		[JsonProperty("ActionsOrTitles")]
		public ActionsOrTitles ActionsOrTitles { get; set; }

		[JsonProperty("ExtraHours")]
		public ExtraHour[] ExtraHours { get; set; }

		[JsonProperty("PerceptionType")]
		public string PerceptionType { get; set; }

		[JsonProperty("Code")]
		public string Code { get; set; }

		[JsonProperty("Description")]
		public string Description { get; set; }

		[JsonProperty("TaxedAmount")]
		public decimal TaxedAmount { get; set; }

		[JsonProperty("ExemptAmount")]
		public decimal ExemptAmount { get; set; }
	}
}