using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces {
    public interface IJwtTokenService {
        Task<string> GenerateTokenAsync(User user);
    }
}
