using MetricsApi.Domain.ValueObjects;

namespace MetricsApi.Domain.Entities.Users.Events
{
    public record UserCreated(Guid UserId, Email Email);
    public record UserNameChanged(Guid UserId, object Name);
    public record UserEmailChanged(Guid UserId, Email Email);
    public record UserDeactivated(Guid UserId);
    public record UserActivated(Guid UserId);
}
