using FinanceManagerBack.Interfaces;
using FinanceManagerBack.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManagerBack.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext db;

        public UserRepository(ApplicationContext dbContext)
        {
            db = dbContext;
        }

        public async Task Add(User entity)
        {
            await db.Users.AddAsync(entity);
        }

        public async Task AddRole(IdentityUserRole<string> identityUserRole)
        {
            await db.UserRoles.AddAsync(identityUserRole);
        }

        public async Task Delete(string id)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user != null)
            {
                db.Remove(user);
            }
        }

        public async Task<User> Edit(User user, string name)
        {
            var _user = await db.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            if (user != null)
            {
                _user.Name = name;
                db.Users.Update(user);
                return _user;
            }

            return null;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await db.Users.ToListAsync();

            if (users != null)
            {
                return users;
            }

            return null;
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user != null)
            {
                return user;
            }

            return null;
        }


        public async Task<User> GetById(string id)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user != null)
                return user;
            return null;
        }

        public async Task<User> GetByName(string userName)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Name == userName);

            if (user != null)
            {
                return user;
            }

            return null;
        }

        public async Task<IdentityRole> GetRole(string roleName)
        {
            return await db.Roles.FirstOrDefaultAsync(x => x.Name == roleName);
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

        public async Task SaveUser(User entity)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == entity.Id);

            user = entity;

            await Save();
        }
    }
}