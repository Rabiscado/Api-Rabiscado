using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Rabiscado.Application.Adapters.Assas;
using Rabiscado.Application.Contracts;
using Rabiscado.Application.Jobs;
using Rabiscado.Application.Notifications;
using Rabiscado.Application.Services;
using Rabiscado.Core.Settings;
using Rabiscado.Domain.Entities;
using Rabiscado.Infra;
using ScottBrady91.AspNetCore.Identity;

namespace Rabiscado.Application;

public static class DependencyInjection
{
    public static void SetupSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        services.Configure<AzureStorageSettings>(configuration.GetSection("AzureStorageSettings"));
        services.Configure<AssasSettings>(configuration.GetSection("AssasSettings"));
    }

    public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.DbContextConfig(configuration);

        services.ConfigureRepositoriesDependency();

        services
            .AddAutoMapper(Assembly.GetExecutingAssembly());

        services
            .AddScoped<IPasswordHasher<User>, Argon2PasswordHasher<User>>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.ConfigureAssas();

        services
            .AddScoped<INotificator, Notificator>();

        services
            .AddScoped<IUserAuthService, UserAuthService>()
            .AddScoped<IPlanService, PlanService>()
            .AddScoped<ICourseService, CourseService>()
            .AddScoped<ILevelService, LevelService>()
            .AddScoped<IForWhoService, ForWhoService>()
            .AddScoped<IModuleService, ModuleService>()
            .AddScoped<IClassService, ClassService>()
            .AddScoped<IEmailService, EmailService>()
            .AddScoped<IFileService, FileService>()
            .AddScoped<IScheduledpaymentService, ScheduledpaymentService>()
            .AddScoped<IExtractService, ExtractService>()
            .AddScoped<IUserService, UserService>();
    }

    public static void AddQuartzJobs(this IServiceCollection services)
    {
        services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        services
            .ConfigureOptions<ScheduledPaymentBackgroundSetup>()
            .ConfigureOptions<CourseBillingBackgroundSetup>();
    }
}