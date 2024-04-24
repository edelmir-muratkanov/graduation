namespace Domain.Projects;

public class ProjectParameter : Entity
{
    public Guid ProjectId { get; private set; }
    public Guid PropertyId { get; private set; }
    public double Value { get; private set; }

    private ProjectParameter(Guid id, Guid projectId, Guid propertyId, double value) : base(id)
    {
        ProjectId = projectId;
        PropertyId = propertyId;
        Value = value;
    }

    private ProjectParameter()
    {
    }

    public static Result<ProjectParameter> Create(Guid projectId, Guid propertyId, double value)
    {
        var parameter = new ProjectParameter(Guid.NewGuid(), projectId, propertyId, value);
        return parameter;
    }
}
