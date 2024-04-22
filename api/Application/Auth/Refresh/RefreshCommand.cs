namespace Application.Auth.Refresh;

public record RefreshResponse(string AccessToken, string RefreshToken);

public record RefreshCommand(string AccessToken, string RefreshToken) : ICommand<RefreshResponse>;