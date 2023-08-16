using Microsoft.Extensions.Options;

namespace ProductService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddAuthorization();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddOptions<InventoryClientSettings>()
				.BindConfiguration(InventoryClientSettings.ConfigurationSection)
				.ValidateDataAnnotations()
				.ValidateOnStart();

			builder.Services.AddHttpClient<InventoryClient>((serviceProvider, httpClient) =>
			{
				var inventoryOptions = serviceProvider.GetRequiredService<IOptions<InventoryClientSettings>>().Value;
				httpClient.DefaultRequestHeaders.Add("Authorization", inventoryOptions.AccessToken);
				httpClient.BaseAddress = new Uri(inventoryOptions?.BaseAddress ?? string.Empty);
				httpClient.Timeout = TimeSpan.FromSeconds(inventoryOptions?.RequestTimeout ?? 5);
			});

			builder.Services.AddScoped<IProductRepository, ProductRepository>();

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapProductEndpoints();

			app.Run();
		}
	}
}