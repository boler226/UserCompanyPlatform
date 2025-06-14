namespace Contracts.Events {
    public record UserRegistered(Guid UserId, string Email, DateTime RegistredAt);
}
