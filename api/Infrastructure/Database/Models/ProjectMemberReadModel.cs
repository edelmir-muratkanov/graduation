using Domain.Users;

namespace Infrastructure.Database.Models;

internal class ProjectMemberReadModel
{
    public Guid ProjectId { get; set; }
    public Guid MemberId { get; set; }
    public UserReadModel Member { get; set; }
}