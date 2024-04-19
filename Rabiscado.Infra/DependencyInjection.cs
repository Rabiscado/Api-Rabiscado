using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rabiscado.Core.Authorization.AuthenticatedUser;
using Rabiscado.Core.Extensions;
using Rabiscado.Domain.Contracts;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Infra.Contexts;
using Rabiscado.Infra.Repositories;

namespace Rabiscado.Infra;

public static class DependencyInjection
{
    public static void DbContextConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<IAuthenticatedUser>(sp =>
        {
            var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();

            return httpContextAccessor.AuthenticatedUser()
                ? new AuthenticatedUser(httpContextAccessor)
                : new AuthenticatedUser();
        });
        
        services.AddDbContext<RabiscadoContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            options.UseMySql(connectionString, serverVersion);
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });
    }

    public static void ConfigureRepositoriesDependency(this IServiceCollection services)
    {
        services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<ICourseRepository, CourseRepository>()
            .AddScoped<ILevelRepository, LevelRepository>()
            .AddScoped<IForWhoRepository, ForWhoRepository>()
            .AddScoped<IModuleRepository, ModuleRepository>()
            .AddScoped<IUserClassRepository, UserClassRepository>()
            .AddScoped<IClassRepository, ClassRepository>()
            .AddScoped<IScheduledpaymentRepository, ScheduledpaymentRepository>()
            .AddScoped<IExtractRepository, ExtractRepository>()
            .AddScoped<IUserPlanSubscriptionRepository, UserPlanSubscriptionRepository>()
            .AddScoped<IExtractReceiptRepository, ExtractReceiptRepository>()
            .AddScoped<ISubscriptionRepository, SubscriptionRepository>()
            .AddScoped<IReimbursementRepository, ReimbursementRepository>()
            .AddScoped<IPlanRepository, PlanRepository>();
    }
}