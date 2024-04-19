using System.Reflection;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Rabiscado.Core.Authorization.AuthenticatedUser;
using Rabiscado.Domain.Contracts;
using Rabiscado.Domain.Entities;
using Rabiscado.Infra.Converters;
using Rabiscado.Infra.Extensions;
using Module = Rabiscado.Domain.Entities.Module;

namespace Rabiscado.Infra.Contexts;

public class RabiscadoContext : DbContext, IUnitOfWork
{
    public RabiscadoContext(DbContextOptions<RabiscadoContext> options, IAuthenticatedUser authenticatedUser) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Plan> Plans { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Level> Levels { get; set; } = null!;
    public DbSet<CourseLevel> CourseLevels { get; set; } = null!;
    public DbSet<ForWho> ForWhos { get; set; } = null!;
    public DbSet<CourseForWho> CourseForWhos { get; set; } = null!;
    public DbSet<Module> Modules { get; set; } = null!;
    public DbSet<Class> Classes { get; set; } = null!;
    public DbSet<Step> Steps { get; set; } = null!;
    public DbSet<Subscription> Subscriptions { get; set; } = null!;
    public DbSet<Scheduledpayment> Scheduledpayments { get; set; } = null!;
    public DbSet<Extract> Extracts { get; set; } = null!;
    public DbSet<ExtractType> ExtractTypes { get; set; } = null!;
    public DbSet<Reimbursement> Reimbursements { get; set; } = null!;
    public DbSet<UserPlanSubscription> UserPlanSubscriptions { get; set; } = null!;
    public DbSet<ExtractReceipt> ExtractReceipts { get; set; } = null!;
    public DbSet<UserClass> UserClasses { get; set; } = null!;
    
    public async Task<bool> Commit() => await SaveChangesAsync() > 0;
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        ApplyTrackingChanges();
        return base.SaveChangesAsync(cancellationToken);
    }
    
    private static void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.ApplyEntityConfiguration();
        modelBuilder.ApplyTrackingConfiguration();
        modelBuilder.ApplySoftDeleteConfiguration();
    }
    
    private void ApplyTrackingChanges()
    {
        var entries = ChangeTracker.Entries().Where(e =>
            e.Entity is ITracking && e.State is EntityState.Modified || e.State is EntityState.Added);

        foreach (var entityEntry in entries)
        {
            ((ITracking)entityEntry.Entity).UpdateAt = DateTime.Now;
            if (entityEntry.State != EntityState.Added)
                continue;

            ((ITracking)entityEntry.Entity).CreateAt = DateTime.Now;
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly())
            .HasCharSet("utf8mb4")
            .UseCollation("utf8mb4_0900_ai_ci")
            .UseGuidCollation(string.Empty);

        ApplyConfigurations(modelBuilder);
        
        base.OnModelCreating(modelBuilder);
    }
    
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<DateOnly>()
            .HaveConversion<DateOnlyCustomConverter>()
            .HaveColumnType("DATE");

        configurationBuilder
            .Properties<TimeOnly>()
            .HaveConversion<TimeOnlyCustomConverter>()
            .HaveColumnType("TIME");
    }
}