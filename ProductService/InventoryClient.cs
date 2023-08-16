namespace ProductService
{
	public sealed class InventoryClient
	{
		private readonly HttpClient _httpClient;

		public InventoryClient(HttpClient client)
		{
			_httpClient = client;
		}

		public async Task<BasicInventory?> GetByProductIdAsync(Guid productId)
		{
			var inventory = await _httpClient.GetFromJsonAsync<BasicInventory>($"inventory/basic/{productId}");
			return inventory;
		}
	}
}
