namespace Api.Features.Method;

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
}