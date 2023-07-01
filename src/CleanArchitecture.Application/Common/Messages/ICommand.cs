using ErrorOr;

using MediatR;

namespace CleanArchitecture.Application.Common.Messages;

internal interface ICommand<out TResponse> : IRequest<TResponse>
    where TResponse : IErrorOr
{
}