using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces {
    public interface IUserRepository {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
