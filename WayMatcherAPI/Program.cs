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
            builder.Services.AddScoped<IEventService, EventService>();

            builder.Services.AddSingleton<ModelMapper>();

            builder.Services.AddScoped<EmailServerDto>();

            // Add CORS services and configure the policy.  Place this BEFORE Authentication.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowSpecificOrigin", // Use a descriptive name
                                policy =>
                                {
                                    policy.WithOrigins(["http://localhost:4000", "https://waymatcher.hobedere.com"]) // Your React app's origin
                                            .AllowAnyMethod()
                                            .AllowAnyHeader()
                                            .AllowCredentials(); // IMPORTANT: Allow credentials
                                });
            });

            // Add Certificate Authentication
            // builder.WebHost.ConfigureKestrel(serverOptions =>
            // {
            //     serverOptions.ConfigureHttpsDefaults(httpsOptions =>
            //     {
            //         var configurationService = builder.Services.BuildServiceProvider().GetRequiredService<ConfigurationService>();
            //         httpsOptions.ServerCertificate = new X509Certificate2(configurationService.GetCertificate());
            //     });
            // });
            builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            // IMPORTANT: UseCors MUST come BEFORE routing and authorization, and before UseHttpsRedirection
            app.UseCors("AllowAnyOrigin");

            // Check if the request is an OPTIONS request and skip redirection if it is.
            app.Use(async (context, next) =>
            {
                if (context.Request.Method != "OPTIONS")
                {
                    //If we are not in Dev, redirect to https
                    if (!builder.Environment.IsDevelopment())
                    {
                        context.Response.Redirect("https://" + context.Request.Host + context.Request.Path + context.Request.QueryString);
                        return; //Short-circuit
                    }
                }

                await next();
            });

            // app.UseHttpsRedirection();

            // Add authentication middleware
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}