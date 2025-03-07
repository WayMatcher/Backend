using Microsoft.Extensions.Configuration;
using WayMatcherBL.DtoModels;

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
        public EmailServerDto GetEmailServer()
        {
            return new EmailServerDto
            {
                Host = _configuration["EmailServer:Host"],
                Port = int.TryParse(_configuration["EmailServer:Port"], out int port) ? port : 587,
                Username = _configuration["EmailServer:Username"],
                Password = _configuration["EmailServer:Password"]
            };
        }
    }
}   