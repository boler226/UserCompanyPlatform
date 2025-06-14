using UserService.Domain.Interfaces;
using UserService.Infrastructure.DbContext;
using UserService.Infrastructure.UnitOfWork.Interfaces;

namespace UserService.Infrastructure.UnitOfWork {
    public class UnitOfWork(
        UserDbContext context,
        IUserRepository userRepository
        ) : IUnitOfWork {
        public IUserRepository Users => userRepository;

        public async Task<int> SaveChangesAsync() =>
            await context.SaveChangesAsync();
    }
}
