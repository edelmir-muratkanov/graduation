using System.Text.Json.Serialization;

namespace Shared;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortOrder
{
    Asc = 1,
    Desc = 2
}
