namespace Facturama.Models.Response
{
    using Facturama.Models.Complements;
    using Newtonsoft.Json;

    public class Cfdi
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("CfdiType")]
        public string CfdiType { get; set; }

        [JsonProperty("Serie")]
        public string Serie { get; set; }

        [JsonProperty("Folio")]
        public string Folio { get; set; }

        [JsonProperty("Date")]
        public string Date { get; set; }

        [JsonProperty("CertNumber")]
        public string CertNumber { get; set; }

        [JsonProperty("PaymentTerms")]
        public string PaymentTerms { get; set; }

        [JsonProperty("PaymentConditions")]
        public string PaymentConditions { get; set; }

        [JsonProperty("PaymentMethod")]
        public string PaymentMethod { get; set; }

        [JsonProperty("PaymentAccountNumber")]
        public string PaymentAccountNumber { get; set; }

        [JsonProperty("ExpeditionPlace")]
        public string ExpeditionPlace { get; set; }

        [JsonProperty("ExchangeRate")]
        public decimal ExchangeRate { get; set; }

        [JsonProperty("Currency")]
        public string Currency { get; set; }

        [JsonProperty("Subtotal")]
        public decimal Subtotal { get; set; }

        [JsonProperty("Discount")]
        public decimal Discount { get; set; }

        [JsonProperty("Total")]
        public decimal Total { get; set; }

        [JsonProperty("Observations")]
        public string Observations { get; set; }

        [JsonProperty("Issuer")]
        public Issuer Issuer { get; set; }

        [JsonProperty("Receiver")]
        public Receiver Receiver { get; set; }

        [JsonProperty("Items")]
        public Item[] Items { get; set; }

        [JsonProperty("Taxes")]
        public Tax[] Taxes { get; set; }

        [JsonProperty("Complement")]
        public Complement Complement { get; set; }

		[JsonProperty("SendMail")]
		public bool SendMail { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("OriginalString")]
        public string OriginalString { get; set; }

        
    }

    public class Complement
    {
        [JsonProperty("TaxStamp")]
        public TaxStamp TaxStamp { get; set; }
    }

    public class TaxStamp
    {
        [JsonProperty("Uuid")]
        public string Uuid { get; set; }

        [JsonProperty("Date")]
        public string Date { get; set; }

        [JsonProperty("CfdiSign")]
        public string CfdiSign { get; set; }

        [JsonProperty("SatCertNumber")]
        public string SatCertNumber { get; set; }

        [JsonProperty("SatSign")]
        public string SatSign { get; set; }
    }

    public class Issuer
    {
        [JsonProperty("FiscalRegime")]
        public string FiscalRegime { get; set; }

        [JsonProperty("ComercialName")]
        public string ComercialName { get; set; }

        [JsonProperty("Rfc")]
        public string Rfc { get; set; }

        [JsonProperty("TaxName")]
        public string TaxName { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("TaxAddress")]
        public Address TaxAddress { get; set; }

        [JsonProperty("IssuedIn")]
        public Address IssuedIn { get; set; }

        [JsonProperty("UrlLogo")]
        public string UrlLogo { get; set; }
    }

    public class Item
    {
        [JsonProperty("Quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty("Unit")]
        public string Unit { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("UnitValue")]
        public decimal UnitValue { get; set; }

        [JsonProperty("Total")]
        public decimal Total { get; set; }

        [JsonProperty("IdentificationNumber")]
        public decimal IdentificationNumber { get; set; }

        [JsonProperty("Complement")]
        public ItemComplement Complement { get; set; }
    }

    public class Receiver
    {
        [JsonProperty("Address")]
        public Address Address { get; set; }

        [JsonProperty("Rfc")]
        public string Rfc { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }
    }

    public class Tax
    {
        [JsonProperty("Total")]
        public decimal Total { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Rate")]
        public decimal Rate { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }
    }
}


