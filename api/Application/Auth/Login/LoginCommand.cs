namespace Application.Auth.Login;

public record LoginResponse(Guid Id, string Email, string Role, string AccessToken, string RefreshToken);

public sealed record LoginCommand(string Email, string Password) : ICommand<LoginResponse>;