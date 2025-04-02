using Microsoft.Extensions.Configuration;
using WayMatcherBL.DtoModels;

namespace WayMatcherBL.Services
{
    /// <summary>
    /// Provides configuration services for the application.
    /// </summary>
    public class ConfigurationService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationService"/> class.
        /// </summary>
        /// <exception cref="FileNotFoundException">Thrown when the appsettings.json file is not found.</exception>
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

        /// <summary>
        /// Finds a file in the specified directory or its subdirectories.
        /// </summary>
        /// <param name="directory">The directory to search in.</param>
        /// <param name="fileName">The name of the file to find.</param>
        /// <returns>The file path if found; otherwise, null.</returns>
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

        /// <summary>
        /// Retrieves the connection string for the specified name.
        /// </summary>
        /// <param name="name">The name of the connection string.</param>
        /// <returns>The connection string.</returns>
        public string GetConnectionString(string name)
        {
            Console.WriteLine($"Retrieving connection string for: {name}");
            return _configuration.GetConnectionString(name);
        }

        /// <summary>
        /// Retrieves the email server settings.
        /// </summary>
        /// <returns>The email server settings.</returns>
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

        /// <summary>
        /// Retrieves the secret key for JWT.
        /// </summary>
        /// <returns>The secret key.</returns>
        public string GetSecretKey()
        {
            return _configuration["Jwt:JwtKey"];
        }

        /// <summary>
        /// Retrieves the certificate path for HTTPS.
        /// </summary>
        /// <returns>The certificate path.</returns>
        public string GetCertificate()
        {
            return _configuration["Kestrel:Endpoints:Https:Certificate:Path"];
        }
    }
}