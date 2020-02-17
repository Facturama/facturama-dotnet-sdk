using Newtonsoft.Json;

namespace Facturama.Models.Complements.Payroll
{
	public class DeductionsDetail
	{
		[JsonProperty("DeduccionType")]
		public string DeduccionType { get; set; }

		[JsonProperty("Code")]
		public string Code { get; set; }

		[JsonProperty("Description")]
		public string Description { get; set; }

		[JsonProperty("Amount")]
		public decimal Amount { get; set; }
	}
}