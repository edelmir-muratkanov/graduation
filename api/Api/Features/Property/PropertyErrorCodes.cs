namespace Api.Features.Property;

public static class PropertyErrorCodes
{
    public static class Create
    {
        public const string MissingName = nameof(MissingName);
        public const string LongName = nameof(LongName);

        public const string MissingUnit = nameof(MissingUnit);
        public const string LongUnit = nameof(LongUnit);
    }
}