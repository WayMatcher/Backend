using Microsoft.Extensions.Configuration;
using WayMatcherBL.DtoModels;

namespace WayMatcherBL.Services
{
    public class ConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService()
        {
            string solutionPath = GetSolutionPath();
            string basePath = Path.Combine(solutionPath, "WayMatcher");
            _configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();
        }

        private string GetSolutionPath()
        {
            string directory = AppContext.BaseDirectory;
            while (!Directory.GetFiles(directory, "*.sln").Any())
            {
                directory = Directory.GetParent(directory).FullName;
            }
            return directory;
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