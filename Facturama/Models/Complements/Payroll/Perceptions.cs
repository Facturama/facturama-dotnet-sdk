using Newtonsoft.Json;

namespace Facturama.Models.Complements.Payroll
{
	public class Perceptions
	{
		[JsonProperty("Details")]
		public PerceptionsDetail[] Details { get; set; }

		[JsonProperty("Retirement")]
		public Retirement Retirement { get; set; }

		[JsonProperty("Indemnification")]
		public Indemnification Indemnification { get; set; }
	}
}