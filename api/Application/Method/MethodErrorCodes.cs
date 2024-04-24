namespace Application.Method;

public static class MethodErrorCodes
{
    public static class Create
    {
        public const string MissingName = nameof(MissingName);
        public const string LongName = nameof(LongName);

        public const string MissingCollectorTypes = nameof(MissingCollectorTypes);
        public const string InvalidCollectorType = nameof(InvalidCollectorType);

        public const string MissingParameters = nameof(MissingParameters);

        public const string MissingParameter = nameof(MissingParameter);

        public const string MissingProperty = nameof(MissingProperty);
        public const string MissingFirstOrSecondParameters = nameof(MissingFirstOrSecondParameters);
    }

    public static class Update
    {
        public const string MissingId = nameof(MissingId);

        public const string LongName = nameof(LongName);

        public const string InvalidCollectorType = nameof(InvalidCollectorType);
    }

    public static class GetById
    {
        public const string MissingId = nameof(MissingId);
    }

    public static class Delete
    {
        public const string MissingId = nameof(MissingId);
    }

    public static class RemoveParameter
    {
        public const string MissingMethodId = nameof(MissingMethodId);
        public const string MissingParameterId = nameof(MissingParameterId);
    }
}