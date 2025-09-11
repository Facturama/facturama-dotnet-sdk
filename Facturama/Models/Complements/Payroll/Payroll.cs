using System;
using Facturama.Services.BaseService;
using Newtonsoft.Json;

namespace Facturama.Models.Complements.Payroll
{
	public class Payroll
	{
		[JsonProperty("Issuer")]
		public Issuer Issuer { get; set; }

		[JsonProperty("Employee")]
		public Employee Employee { get; set; }

		[JsonProperty("Perceptions")]
		public Perceptions Perceptions { get; set; }

		[JsonProperty("Deductions")]
		public Deductions Deductions { get; set; }

		[JsonProperty("OtherPayments")]
		public OtherPayment[] OtherPayments { get; set; }

		[JsonProperty("Incapacities")]
		public Incapacity[] Incapacities { get; set; }

		[JsonProperty("Type")]
		public string Type { get; set; }

		[JsonProperty("PaymentDate")]
		[JsonConverter(typeof(DateFormatConverter), "yyyy-MM-ddTHH:mm:ss")]
		public DateTime PaymentDate { get; set; }

		[JsonProperty("InitialPaymentDate")]
		[JsonConverter(typeof(DateFormatConverter), "yyyy-MM-ddTHH:mm:ss")]
		public DateTime InitialPaymentDate { get; set; }

		[JsonProperty("FinalPaymentDate")]
		[JsonConverter(typeof(DateFormatConverter), "yyyy-MM-ddTHH:mm:ss")]
		public DateTime FinalPaymentDate { get; set; }

		[JsonProperty("DaysPaid")]
		public decimal DaysPaid { get; set; }
		
		[JsonProperty("DailySalary")]
		public decimal DailySalary { get; set; }

		[JsonProperty("BaseSalary")]
		public decimal BaseSalary { get; set; }
	}
}
