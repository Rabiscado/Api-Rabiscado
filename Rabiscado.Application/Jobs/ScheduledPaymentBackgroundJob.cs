using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;
using Rabiscado.Application.Contracts;

namespace Rabiscado.Application.Jobs;

[DisallowConcurrentExecution]
public class ScheduledPaymentBackgroundJob : IJob
{
    private readonly IServiceProvider _serviceProvider;

    public ScheduledPaymentBackgroundJob(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        await userService.GenerateScheduledPayment();
    }
}

public class ScheduledPaymentBackgroundSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        options
            .AddJob<ScheduledPaymentBackgroundJob>(jobBuilder => jobBuilder.WithIdentity(JobKey.Create(nameof(ScheduledPaymentBackgroundJob))))
            .AddTrigger(trigger => trigger
                .ForJob(JobKey.Create(nameof(ScheduledPaymentBackgroundJob)))
                .WithSimpleSchedule(schedule => schedule
                    .WithInterval(TimeSpan.FromDays(1))
                    .RepeatForever()));
    }
}