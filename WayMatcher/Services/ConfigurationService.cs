using Microsoft.Extensions.Configuration;

namespace WayMatcherBL.Services
{
    public class ConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath("C:\\Users\\christian.rudigier\\source\\repos\\WayMatcher\\Backend\\WayMatcher")
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public string GetConnectionString(string name)
        {
            return _configuration.GetConnectionString(name);
        }
    }
}   