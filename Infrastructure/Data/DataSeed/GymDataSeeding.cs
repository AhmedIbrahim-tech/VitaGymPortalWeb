using Infrastructure.Contexts;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Data.DataSeed
{
    public static class GymDataSeeding
    {
        public static bool SeedData(GymDbContext gymDbContext)
        {
            try
            {
                if(!gymDbContext.Plans.Any())
                {
                    var plans = LOadDataFromFile<Plan>("plans.json");
                    gymDbContext.Plans.AddRange(plans);
                    gymDbContext.SaveChanges();
                }
                if(!gymDbContext.Categories.Any())
                {
                    var categories = LOadDataFromFile<Category>("categories.json");
                    gymDbContext.Categories.AddRange(categories);
                    gymDbContext.SaveChanges();
                }

                return gymDbContext.SaveChanges() > 0;

            }
            catch (Exception)
            {
                return false;
            }
        }

        private static List<T> LOadDataFromFile<T>(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files", fileName);
            if (!File.Exists(filePath))
                throw new FileNotFoundException();

            var jsonData = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            options.Converters.Add(new JsonStringEnumConverter());
            return JsonSerializer.Deserialize<List<T>>(jsonData, options) ?? new List<T>();
        }
    }
}
