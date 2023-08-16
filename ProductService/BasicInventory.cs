namespace ProductService
{
	public class BasicInventory
	{
		public Guid ProductId { get; set; }
		public string? SKU { get; set; }
		public int InStock { get; set; }
	}
}
