using Newtonsoft.Json;

namespace Facturama.Models.Complements.Payroll
{
	public class ExtraHour
	{
		[JsonProperty("Days")]
		public int Days { get; set; }

		[JsonProperty("HoursType")]
		public string HoursType { get; set; }

		[JsonProperty("ExtraHours")]
		public int ExtraHours { get; set; }

		[JsonProperty("PaidAmount")]
		public decimal PaidAmount { get; set; }
	}
}