using Newtonsoft.Json;

namespace Facturama.Models.Complements.Payroll
{
	public class Issuer
	{
		[JsonProperty("EntitySNCF")]
		public EntitySncf EntitySncf { get; set; }

		[JsonProperty("Curp")]
		public string Curp { get; set; }

		[JsonProperty("EmployerRegistration")]
		public string EmployerRegistration { get; set; }

		[JsonProperty("FromEmployerRfc")]
		public string FromEmployerRfc { get; set; }
	}
}