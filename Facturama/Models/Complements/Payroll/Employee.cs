using System;
using Facturama.Services.BaseService;
using Newtonsoft.Json;

namespace Facturama.Models.Complements.Payroll
{
	public class Employee
	{
		[JsonProperty("Outsourcing")]
		public Outsourcing[] Outsourcing { get; set; }

		[JsonProperty("Curp")]
		public string Curp { get; set; }

		[JsonProperty("SocialSecurityNumber")]
		public string SocialSecurityNumber { get; set; }

		[JsonProperty("StartDateLaborRelations")]
		[JsonConverter(typeof(DateFormatConverter), "yyyy-MM-ddTHH:mm:ss")]
		public DateTime? StartDateLaborRelations { get; set; }

		[JsonProperty("ContractType")]
		public string ContractType { get; set; }

		[JsonProperty("Unionized")]
		public bool Unionized { get; set; }

		[JsonProperty("TypeOfJourney")]
		public string TypeOfJourney { get; set; }

		[JsonProperty("RegimeType")]
		public string RegimeType { get; set; }

		[JsonProperty("EmployeeNumber")]
		public string EmployeeNumber { get; set; }

		[JsonProperty("Department")]
		public string Department { get; set; }

		[JsonProperty("Position")]
		public string Position { get; set; }

		[JsonProperty("PositionRisk")]
		public string PositionRisk { get; set; }

		[JsonProperty("FrequencyPayment")]
		public string FrequencyPayment { get; set; }

		[JsonProperty("Bank")]
		public string Bank { get; set; }

		[JsonProperty("BankAccount")]
		public string BankAccount { get; set; }

		[JsonProperty("BaseSalary")]
		public decimal BaseSalary { get; set; }

		[JsonProperty("DailySalary")]
		public decimal? DailySalary { get; set; }

		[JsonProperty("FederalEntityKey")]
		public string FederalEntityKey { get; set; }
	}
}