namespace ProductService
{
	public static class Endpoints
	{
		public static void MapProductEndpoints(this IEndpointRouteBuilder app)
		{
			app.MapGet("/product/info/{id}", async (
				Guid id, 
				IProductRepository productRepo, 
				InventoryClient inventoryService) =>
			{
				var product = productRepo.GetProductInfo(id);
				if(product != null)
				{
					var inventory = await inventoryService.GetByProductIdAsync(id);
					if(inventory != null)
					{
						product.SKU = inventory.SKU;
						product.InStock = inventory.InStock;
					}
				}
				return product;
			})
			.WithName("GetProductInfo")
			.WithOpenApi();
		}
	}
}
