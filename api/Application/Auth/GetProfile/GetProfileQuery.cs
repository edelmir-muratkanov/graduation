namespace Application.Auth.GetProfile;

public record GetProfileResponse(string Id, string Email, string Role);

public sealed record GetProfileQuery : IQuery<GetProfileResponse>;