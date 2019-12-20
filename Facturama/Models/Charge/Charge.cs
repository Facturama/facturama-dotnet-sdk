
namespace Facturama.Models.Charge
{
	public class Charge
	{		
		public string Id { get; set; }		
		public InvoiceData InvoiceData { get; set; }		
		public string PaymentType { get; set; }
		public string CardId { get; set; }		
		public string PromoCode { get; set; }		
		public Item[] Items { get; set; }		
		public decimal TotalAmount { get; set; }		
		public string Description { get; set; }		
		public System.DateTime Date { get; set; }		
		public string Status { get; set; }		
		public string PaymentReceiptPdfUrl { get; set; }		
		public string PaymentReceiptHtmlUrl { get; set; }		
		public decimal Iva { get; set; }		
		public decimal Discount { get; set; }		
		public decimal Subtotal { get; set; }		
		public string Reference { get; set; }
	}
}




