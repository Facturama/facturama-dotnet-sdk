using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Facturama.Services;
using System.Collections.Generic;

namespace Facturama.Models.Request
{
    public enum CfdiType
    {
        [EnumMember(Value = "I")]
        Ingreso,

        [EnumMember(Value = "E")]
        Egreso,

        [EnumMember(Value = "T")]
        Traslado,

        [EnumMember(Value = "P")]
        Pago
    }

    public class Cfdi 
    {
        [JsonProperty("NameId")]
        public string NameId { get; set; }

        [JsonProperty("Date")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-ddTHH:mm:ss")]
        public DateTime Date { get; set; }

        [JsonProperty("Serie")]
        public string Serie { get; set; }

        [JsonProperty("PaymentAccountNumber")]
        public string PaymentAccountNumber { get; set; }

        [JsonProperty("CurrencyExchangeRate")]
        public decimal? CurrencyExchangeRate { get; set; }

        [JsonProperty("Currency")]
        public string Currency { get; set; }

        [JsonProperty("ExpeditionPlace")]
        public string ExpeditionPlace { get; set; }

        [JsonProperty("PaymentConditions")]
        public string PaymentConditions { get; set; }

        [JsonProperty("Relations")]
        public CfdiRelations Relations { get; set; }

        [JsonProperty("CfdiType")]
        public CfdiType CfdiType { get; set; }

        [JsonProperty("PaymentForm")]
        public string PaymentForm { get; set; }

        [JsonProperty("PaymentMethod")]
        public string PaymentMethod { get; set; }

        [JsonProperty("Receiver")]
        public Receiver Receiver { get; set; }

        [JsonProperty("Items")]
        public List<Item> Items { get; set; }

        [JsonProperty("Observations")]
        public string Observations { get; set; }

        [JsonProperty("OrderNumber")]
        public string OrderNumber { get; set; }

        [JsonProperty("PaymentBankName")]
        public string PaymentBankName { get; set; }

        [JsonProperty("Complemento")]
        public Complement Complement { get; set; }
    }
}
