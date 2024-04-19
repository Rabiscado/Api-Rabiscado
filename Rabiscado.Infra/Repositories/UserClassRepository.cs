using Microsoft.EntityFrameworkCore;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;
using Rabiscado.Infra.Contexts;

namespace Rabiscado.Infra.Repositories;

public class UserClassRepository : Repository<UserClass>, IUserClassRepository
{
    public UserClassRepository(RabiscadoContext context) : base(context)
    {
    }

    public async Task<UserClass?> GetById(int classId, int userId)
    {
        return await Context.UserClasses
            .Include(x => x.User)
            .Include(x => x.Class)
            .FirstOrDefaultAsync(x => x.ClassId == classId && x.UserId == userId);
    } 

    public void MarkAsWatched(UserClass userClass)
    {
        userClass.Watched = true;
        Context.UserClasses.Update(userClass);
    }

    public void MarkAsUnwatched(UserClass userClass)
    {   
        userClass.Watched = false;
        Context.UserClasses.Update(userClass);
    }
}