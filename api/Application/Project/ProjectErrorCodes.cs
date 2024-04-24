namespace Application.Project;

public static class ProjectErrorCodes
{
    public static class GetById
    {
        public const string MissingId = nameof(MissingId);
    }

    public static class Create
    {
        public const string MissingName = nameof(MissingName);

        public const string MissingOperator = nameof(MissingOperator);

        public const string MissingCountry = nameof(MissingCountry);

        public const string MissingCollectorType = nameof(MissingCollectorType);
        public const string InvalidCollectorType = nameof(InvalidCollectorType);

        public const string MissingProjectType = nameof(MissingProjectType);
        public const string InvalidProjectType = nameof(InvalidProjectType);

        public const string MissingMethods = nameof(MissingMethods);

        public const string MissingParameters = nameof(MissingParameters);

        public const string MissingParameter = nameof(MissingParameter);
        public const string MissingProperty = nameof(MissingProperty);
        public const string MissingParameterValue = nameof(MissingParameterValue);
    }
}