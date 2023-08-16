namespace ProductService
{
	public class InventoryClientSettings
	{
		public const string ConfigurationSection = "InventoryClientSettings";
		public string? AccessToken { get; set; }
		public string? BaseAddress { get; set; }
		public int RequestTimeout {get; set; }
	}
}
