using Shared;

namespace Api.Contracts.Property;

public record PropertyResponse(Guid Id, string Name, string Unit);

public class GetPropertiesResponse(List<PropertyResponse> items, int count, int pageNumber, int pageSize)
    : PaginatedList<PropertyResponse>(items, count, pageNumber, pageSize);
