using Api.Shared.Models;
using MediatR;

namespace Api.Shared.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;