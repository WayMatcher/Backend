using Microsoft.EntityFrameworkCore;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;
using WayMatcherBL.Mapper;
using WayMatcherBL.Models;
using WayMatcherBL.Services;

class Program
{
    static void Main(string[] args)
    {
        var configService = new ConfigurationService();
        var connectionString = configService.GetConnectionString("DefaultConnection");

        var options = new DbContextOptionsBuilder<WayMatcherContext>().UseSqlServer(connectionString).Options;

        var dbContext = new WayMatcherContext(options);
        var modelMapper = new ModelMapper();

        IDatabaseService databaseService = new DatabaseService(dbContext, modelMapper);
        IEmailService emailService = new EmailService(configService.GetEmailServer());
        IUserService userService = new UserService(databaseService, emailService);

        var user = new UserDto
        {
            UserId = 1,
            Username = "TestUser",
            EMail = "TestEmail@gmail.com",
            Password = "TestPassword",
        };

        userService.LoginUser(user);
    }
}
