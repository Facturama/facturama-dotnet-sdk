using Newtonsoft.Json;

namespace Facturama.Models.Complements.Payroll
{
	public class Deductions
	{
		[JsonProperty("Details")]
		public DeductionsDetail[] Details { get; set; }
	}
}