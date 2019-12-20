
namespace Facturama.Models.Charge
{
	public class Item
	{
		public string ProductId { get; set; }
		public int Quantity { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
		public decimal Price { get; set; }
		public decimal Subtotal { get; set; }
		public decimal Iva { get; set; }
	}
}
