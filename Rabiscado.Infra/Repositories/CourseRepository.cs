using Microsoft.EntityFrameworkCore;
using Rabiscado.Domain.Contracts.Pagination;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;
using Rabiscado.Infra.Contexts;
using Rabiscado.Infra.Extensions;

namespace Rabiscado.Infra.Repositories;

public class CourseRepository : Repository<Course>, ICourseRepository
{
    public CourseRepository(RabiscadoContext context) : base(context)
    {
    }

    public async Task<Course?> GetById(int id)
    {
        return await Context.Courses.AsNoTrackingWithIdentityResolution()
            .Include(c => c.CourseLevels).ThenInclude(cl => cl.Level)
            .Include(c => c.CourseForWhos).ThenInclude(cl => cl.ForWho)
            .Include(c => c.Modules).ThenInclude(c => c.Classes).ThenInclude(c => c.Steps)
            .Include(c => c.Subscriptions)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Course>> GetAll()
    {
        return await Context.Courses
            .Include(c => c.CourseLevels).ThenInclude(cl => cl.Level)
            .Include(c => c.CourseForWhos).ThenInclude(cl => cl.ForWho)
            .Include(c=> c.Modules).ThenInclude(c => c.Classes).ThenInclude(c => c.Steps)
            .Include(c => c.Subscriptions)
            .ToListAsync();
    }

    public override async Task<IResultPaginated<Course>> Search(ISearchPaginated<Course> filtro)
    {
        var queryable = Context.Courses
            .Include(c => c.CourseLevels).ThenInclude(cl => cl.Level)
            .Include(c => c.CourseForWhos).ThenInclude(cl => cl.ForWho)
            .Include(c => c.Subscriptions)
            .AsQueryable();

        filtro.ApplyFilter(ref queryable);
        filtro.ApplyOrdernation(ref queryable);

        return await queryable.SearchPaginatedAsync(filtro.Page, filtro.PageSize);
    }

    public void Create(Course plan)
    {
        Context.Courses.Add(plan);
    }

    public void Update(Course plan)
    {
        Context.Courses.Update(plan);
    }

    public void Disable(Course plan)
    {
        plan.Disabled = true;
        Context.Courses.Update(plan);
    }

    public void Active(Course plan)
    {
        plan.Disabled = false;
        Context.Courses.Update(plan);
    }

    public void Delete(Course plan)
    {
        Context.Courses.Remove(plan);
    }
}