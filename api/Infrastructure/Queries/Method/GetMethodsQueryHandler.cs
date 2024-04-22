using System.Linq.Expressions;
using Application.Abstractions.Messaging;
using Application.Method.GetMethods;
using Infrastructure.Database;
using Infrastructure.Database.Models;
using Shared;
using Shared.Results;

namespace Infrastructure.Queries.Method;
//
// internal class GetMethodsQueryHandler(ApplicationReadDbContext dbContext)
//     : IQueryHandler<GetMethodsQuery, PaginatedList<GetMethodsResponse>>
// {
//     public async Task<Result<PaginatedList<GetMethodsResponse>>> Handle(
//         GetMethodsQuery request,
//         CancellationToken cancellationToken)
//     {
//         var methodsQuery = dbContext.Methods.;
//
//         if (!string.IsNullOrWhiteSpace(request.SearchTerm))
//             methodsQuery = methodsQuery.Where(m => m.Name.Contains(request.SearchTerm));
//
//         Expression<Func<MethodReadModel, object>> keySelector = request.SortColumn?.ToLower() switch
//         {
//             "name" => property => property.Name,
//             _ => property => property.CreatedAt
//         };
//
//         methodsQuery = request.SortOrder == SortOrder.Asc
//             ? methodsQuery.OrderBy(keySelector)
//             : methodsQuery.OrderByDescending(keySelector);
//
//         var methods = methodsQuery.Select(m => new GetMethodsResponse
//             {
//                 Id = m.Id,
//                 Name = m.Name,
//                 CollectorTypes = m.CollectorTypes.Select(e => e.ToString()),
//                 Parameters = m.Parameters.Select(p => new GetMethodsParameterResponse
//                 {
//                     PropertyName = dbContext.Properties.FirstOrDefault(property => property.Id == p.Id)!.Name,
//                     First = p.FirstParameters,
//                     Second = p.SecondParameters
//                 }).ToList()
//             }
//         );
//
//         var list = await PaginatedList<GetMethodsResponse>.CreateAsync(
//             methods,
//             (int)request.PageNumber!,
//             (int)request.PageSize!,
//             cancellationToken);
//
//         Console.WriteLine(list.Items);
//         return list;
//     }
// }