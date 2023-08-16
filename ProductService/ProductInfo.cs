namespace ProductService
{
	public class ProductInfo
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public decimal Price { get; set; }
		public string? Currency { get; set;}
		public string? SKU { get; set; }
		public int InStock { get; set; }
	}
}
