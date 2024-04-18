using FinanceManagerBack.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManagerBack.Interfaces
{
    public interface IUserRepository
    {
        Task AddRole(IdentityUserRole<string> identityUserRole);

        Task<IdentityRole> GetRole(string roleName);

        Task<IEnumerable<User>> GetAll();

        Task<User> GetById(string id);

        Task<User> GetByName(string userName);

        Task<User> GetByEmail(string email);

        Task Add(User entity);

        Task<User> Edit(User user, string name);

        Task Delete(string id);

        Task Save();
    }
}