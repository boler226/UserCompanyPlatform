using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.DbContext;

namespace UserService.Infrastructure.Repositories {
    public class UserRepository(UserDbContext context) : IUserRepository {
        public async Task<List<User>> GetAllAsync() =>
            await context.Users.ToListAsync();

        public async Task<User?> GetByIdAsync(Guid id) =>
            await context.Users.FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User?> GetByEmailAsync(string email) =>
            await context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task AddAsync(User user) {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user) {
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user) {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }
    }
}
