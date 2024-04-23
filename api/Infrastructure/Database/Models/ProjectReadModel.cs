using Domain;
using Domain.Projects;

namespace Infrastructure.Database.Models;

internal sealed class ProjectReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string Operator { get; set; }
    public ProjectType ProjectType { get; set; }
    public CollectorType CollectorType { get; set; }
    public List<ProjectParameterReadModel> Parameters { get; set; } = [];
    public List<ProjectMethodReadModel> Methods { get; set; } = [];
    public List<ProjectMemberReadModel> Members { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}