using Api.Shared.Models;
using MediatR;

namespace Api.Shared.Messaging;

public interface IBaseCommand;

public interface ICommand: IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;