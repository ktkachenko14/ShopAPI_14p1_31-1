using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.API.Domain.Models;
using Shop.API.Domain.Repositories;
using Shop.API.Persistence.Contexts;

namespace Shop.API.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(User user)
        {
            await context.Users.AddAsync(user);
        }

        public async Task<User> FindByIdAsync(int id)
        {
            return await context.Users
                                    .Include(x => x.UserRoles)
                                        .ThenInclude(x => x.Role)
                                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await context.Users.Include(x => x.UserRoles)
                                        .ThenInclude(x => x.Role)
                                            .ToListAsync();
        }

        public void Remove(User user)
        {
            context.Users.Remove(user);
        }

        public void Update(User user)
        {
            context.Users.Update(user);
        }
    }
}