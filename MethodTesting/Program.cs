using Microsoft.EntityFrameworkCore;
using WayMatcherBL.Interfaces;
using WayMatcherBL.Mapper;
using WayMatcherBL.Models;
using WayMatcherBL.Services;

class Program
{
    static void Main(string[] args)
    {
        var configService = new ConfigurationService();
        //var connectionString = configService.GetConnectionString("DefaultConnection");

        //var options = new DbContextOptionsBuilder<WayMatcherContext>().UseSqlServer(connectionString).Options;

        //var dbContext = new WayMatcherContext(options);
        //var modelMapper = new ModelMapper();

        //IDatabaseService databaseService = new DatabaseService(dbContext, modelMapper);
        IEmailService emailService = new EmailService(configService.GetEmailServer());

        emailService.SendEmail(new WayMatcherBL.DtoModels.EmailDto
        {
            Username = "Christian",
            To = "christian.rudigier@lbs4.salzburg.at",
            Subject = "JUHU",
            Body = "Test",
            IsHtml = false
        });

    }
}
