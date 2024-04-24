namespace Application.Auth.Register;

public record RegisterResponse(Guid Id, string Email, string Role, string AccessToken, string RefreshToken);

public sealed record RegisterCommand(string Email, string Password) : ICommand<RegisterResponse>;
