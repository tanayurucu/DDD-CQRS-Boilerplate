using ErrorOr;

using MediatR;

namespace CleanArchitecture.Application.Common.Messages;

internal interface IQuery<out TResponse> : IRequest<TResponse> 
    where TResponse : IErrorOr
{
}