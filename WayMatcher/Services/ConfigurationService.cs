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
            
            Console.WriteLine($"Base path: {basePath}");  //log

            _configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();
            Console.WriteLine($"Configuration loaded: {_configuration != null}"); //log
        }

        private string GetSolutionPath()
        {
            string directory = AppContext.BaseDirectory;
            while (!Directory.GetFiles(directory, "*.sln").Any()) //#TODO change how the file gets found
            {
                directory = Directory.GetParent(directory).FullName;
            }
            return directory;
        }

        public string GetConnectionString(string name)
        {
            Console.WriteLine($"Retrieving connection string for: {name}"); //log
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

        public string GetSecretKey()
        {
            return _configuration["Jwt:JwtKey"];
        }
    }
}   