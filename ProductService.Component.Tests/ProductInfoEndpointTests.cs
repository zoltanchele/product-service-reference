using MbDotNet;
using MbDotNet.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace ProductService.Component.Tests
{
	[Collection("SequentialTests")]
	public class ProductInfoEndpointTests
		: IClassFixture<ProductServiceFactory<Program>>
	{
		private readonly HttpClient _httpClient;
		private readonly ProductServiceFactory<Program> _factory;
		private readonly MountebankClient _mountebankClient;

		public ProductInfoEndpointTests(ProductServiceFactory<Program> factory)
		{
			_factory = factory;
			_httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions
			{
				AllowAutoRedirect = false
			});
			_mountebankClient = new MountebankClient();
			_mountebankClient.DeleteAllImpostersAsync().Wait();
		}

		[Fact]
		public async Task Get_ProductInfo_ReturnsProductInfo()
		{
			// arrange
			var productId = Guid.NewGuid();
			var quantity = 6;
			var sku = "ABCD-123456";
			var basicInventory = new BasicInventory
			{
				ProductId = productId,
				SKU = sku,
				InStock = quantity
			};
			await _mountebankClient.CreateHttpImposterAsync(4545, imposter =>
			{
				imposter.AddStub()
					.OnPathAndMethodEqual($"/inventory/basic/{productId}", Method.Get)
					.ReturnsJson(HttpStatusCode.OK, basicInventory);
				imposter.AddStub()
					.ReturnsStatus(HttpStatusCode.NotFound);
			});

			// act
			var response = await _httpClient.GetFromJsonAsync<ProductInfo>($"/product/info/{productId}");
			await Task.Delay(3300);

			// assert
			Assert.NotNull(response);
			Assert.Equal(productId, response.Id);
			Assert.Equal(sku, response.SKU);
			Assert.Equal(quantity, response.InStock);
		}
	}
}
