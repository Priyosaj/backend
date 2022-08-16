using System.Text.Json;
using Microsoft.Extensions.Logging;
using Priyosaj.Core.Entities.ProductEntities;

namespace Priyosaj.Data.Seed;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            if(!context.ProductCategories.Any())
            {
                var categoriesData = File.ReadAllText("../Priyosaj.Data/Seed/SeedData/ProductCategories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
                foreach(var category in categories)
                {
                    context.ProductCategories.Add(category);
                }
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<StoreContextSeed>();
            logger.LogError(ex.Message);
        }
    }
}