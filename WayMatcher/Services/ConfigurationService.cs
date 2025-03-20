using Microsoft.Extensions.Configuration;
using WayMatcherBL.DtoModels;

namespace WayMatcherBL.Services
{
    public class ConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService()
        {
            string basePath = AppContext.BaseDirectory;
            string appSettingsPath = FindFile(basePath, "appsettings.json");

            if (appSettingsPath == null)
            {
                throw new FileNotFoundException("appsettings.json not found.");
            }

            Console.WriteLine($"AppSettings path: {appSettingsPath}"); 

            _configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(appSettingsPath))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            Console.WriteLine($"Configuration loaded: {_configuration != null}"); 
        }

        private string FindFile(string directory, string fileName)
        {
            foreach (var file in Directory.GetFiles(directory))
            {
                if (Path.GetFileName(file) == fileName)
                {
                    return file;
                }
            }

            foreach (var dir in Directory.GetDirectories(directory))
            {
                var result = FindFile(dir, fileName);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        public string GetConnectionString(string name)
        {
            Console.WriteLine($"Retrieving connection string for: {name}");
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