using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;
using Rabiscado.Application.Contracts;

namespace Rabiscado.Application.Jobs;

[DisallowConcurrentExecution]
public class CourseBillingBackgroundJob : IJob
{
    private readonly IServiceProvider _serviceProvider;

    public CourseBillingBackgroundJob(IServiceProvider serviceProvider, IUserService userService)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        await userService.GenerateCourseBilling();
    }
}

public class CourseBillingBackgroundSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        options
            .AddJob<CourseBillingBackgroundJob>(jobBuilder => jobBuilder.WithIdentity(JobKey.Create(nameof(CourseBillingBackgroundJob))))
            .AddTrigger(trigger => trigger
                .ForJob(JobKey.Create(nameof(CourseBillingBackgroundJob)))
                .WithSimpleSchedule(schedule => schedule
                    .WithInterval(TimeSpan.FromDays(1))
                    .RepeatForever()));
    }
}