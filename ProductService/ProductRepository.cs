namespace ProductService
{
	public class ProductRepository : IProductRepository
	{
		public ProductInfo GetProductInfo(Guid productId)
		{
			var product = new ProductInfo
			{
				Id = productId,
				Name = "My Product",
				Description = "Lorem ipsum dolor sit amet.",
				Price = 13.88m,
				Currency = "$"
			};
			return product;
		}
	}

	public interface IProductRepository
	{
		ProductInfo GetProductInfo(Guid productId);
	}
}
