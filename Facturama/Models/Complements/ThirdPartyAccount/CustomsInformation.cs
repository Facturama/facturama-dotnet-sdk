using Newtonsoft.Json;

namespace Facturama.Models.Complements.ThirdPartyAccount
{
	public class CustomsInformation
	{
		[JsonProperty("Number")]
		public string Number { get; set; }

		[JsonProperty("Date")]
		public string Date { get; set; }

		[JsonProperty("Customs")]
		public string Customs { get; set; }
	}
}