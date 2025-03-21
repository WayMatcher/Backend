using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using WayMatcherBL.DtoModels;
using WayMatcherBL.Interfaces;
using WayMatcherBL.Mapper;
using WayMatcherBL.Models;
using WayMatcherBL.Services;

namespace WayMatcherAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.  
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi  
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<WayMatcherContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSingleton<ConfigurationService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IDatabaseService, DatabaseService>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            builder.Services.AddSingleton<ModelMapper>();

            builder.Services.AddScoped<EmailServerDto>();

            // Add Certificate Authentication  
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ConfigureHttpsDefaults(httpsOptions =>
                {
                    var configurationService = builder.Services.BuildServiceProvider().GetRequiredService<ConfigurationService>();
                    httpsOptions.ServerCertificate = new X509Certificate2(configurationService.GetCertificate());
                });
            });
            builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate();

            var app = builder.Build();

            // Configure the HTTP request pipeline.  
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            // Add authentication middleware  
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
