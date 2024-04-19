using Microsoft.EntityFrameworkCore;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;
using Rabiscado.Infra.Contexts;

namespace Rabiscado.Infra.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(RabiscadoContext context) : base(context)
    {
    }

    public async Task<User?> GetById(int id)
    {
        return await Context.Users
            .Include(u => u.Plan)
            .Include(u => u.Scheduledpayments)
            .Include(u => u.Subscriptions)
            .ThenInclude(s => s.Course)
            .ThenInclude(c => c.Modules)
            .ThenInclude(m => m.Classes)
            .ThenInclude(c => c.Steps)
            .Include(u => u.UserPlanSubscriptions)
            .Include(c=> c.Subscriptions).ThenInclude(c=> c.Course).ThenInclude(c=>c.CourseLevels).ThenInclude(cl => cl.Level)
            .Include(c=> c.Subscriptions).ThenInclude(c=> c.Course).ThenInclude(c=>c.CourseForWhos).ThenInclude(cl => cl.ForWho)
            .Include(u=> u.UserClasses)
            .Include(u => u.UserPlanSubscriptions)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await Context.Users
            .Include(u => u.Plan)
            .Include(u => u.Scheduledpayments)
            .Include(u => u.Subscriptions)
            .ThenInclude(s => s.Course)
            .ThenInclude(c => c.Modules)
            .ThenInclude(m => m.Classes)
            .ThenInclude(c => c.Steps)
            .Include(u => u.UserPlanSubscriptions)
            .Include(c=> c.Subscriptions).ThenInclude(c=> c.Course).ThenInclude(c=>c.CourseLevels).ThenInclude(cl => cl.Level)
            .Include(c=> c.Subscriptions).ThenInclude(c=> c.Course).ThenInclude(c=>c.CourseForWhos).ThenInclude(cl => cl.ForWho)
            .Include(u=> u.UserClasses)
            .Include(u => u.UserPlanSubscriptions)
            .FirstOrDefaultAsync(c => c.Email == email);
    }
    
    public async Task<User?> GetByPhone(string phone)
    {
        return await Context.Users
            .Include(u => u.Plan)
            .Include(u => u.Scheduledpayments)
            .Include(u => u.Subscriptions)
            .ThenInclude(s => s.Course)
            .ThenInclude(c => c.Modules)
            .ThenInclude(m => m.Classes)
            .ThenInclude(c => c.Steps)
            .Include(u => u.UserPlanSubscriptions)
            .Include(c=> c.Subscriptions).ThenInclude(c=> c.Course).ThenInclude(c=>c.CourseLevels).ThenInclude(cl => cl.Level)
            .Include(c=> c.Subscriptions).ThenInclude(c=> c.Course).ThenInclude(c=>c.CourseForWhos).ThenInclude(cl => cl.ForWho)
            .Include(u=> u.UserClasses)
            .Include(u => u.UserPlanSubscriptions)
            .FirstOrDefaultAsync(c => c.Phone == phone);
    }

    public async Task<List<User>> GetAll()
    {
        return await Context.Users
            .Include(u => u.Plan)
            .Include(u => u.Scheduledpayments)
            .Include(u => u.Subscriptions)
            .ThenInclude(s => s.Course)
            .ThenInclude(c => c.Modules)
            .ThenInclude(m => m.Classes)
            .ThenInclude(c => c.Steps)
            .Include(u => u.UserPlanSubscriptions)
            .Include(c=> c.Subscriptions).ThenInclude(c=> c.Course).ThenInclude(c=>c.CourseLevels).ThenInclude(cl => cl.Level)
            .Include(c=> c.Subscriptions).ThenInclude(c=> c.Course).ThenInclude(c=>c.CourseForWhos).ThenInclude(cl => cl.ForWho)
            .Include(u=> u.UserClasses)
            .Include(u => u.UserPlanSubscriptions)
            .ToListAsync();
    }

    public void Create(User user)
    {
        Context.Users.Add(user);
    }

    public void Update(User user)
    {
        Context.Users.Update(user);
    }

    public void Disable(User user)
    {
        user.Disabled = false;
        Context.Users.Update(user);
    }

    public void Active(User user)
    {
        user.Disabled = true;
        Context.Users.Update(user);
    }
}