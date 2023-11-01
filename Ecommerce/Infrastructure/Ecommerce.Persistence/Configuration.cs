using Microsoft.Extensions.Configuration;

namespace Ecommerce.Persistence;

public  static class   Configuration
{
    public static string ConnectionString
    {
        get
        {
            ConfigurationManager configurationManager = new ConfigurationManager();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory() , "../../Presentation/Ecommerce.API")); // Nuget //  
            configurationManager.AddJsonFile("appsettings.json"); // Nuget
            return configurationManager.GetConnectionString("PostgreSQL");
        }
    }
}