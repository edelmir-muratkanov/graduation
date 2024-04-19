using System.Text.Json.Serialization;

namespace Api.Shared.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortOrder
{
    Asc = 1,
    Desc = 2,
}