using System.Collections;

namespace Application.Abstractions;

public interface IUseCase<in TRequest, TResponse>
{
    Task<TResponse> Handle(TRequest request);
}

public interface IUseCase<in TId, in TRequest, TResponse>
{
    Task<TResponse> Handle(TId id, TRequest request);
}

public interface IUseCase<in TRequest>
{
    Task Handle(TRequest request);
}

public interface IUseCase
{
    Task<ICollection> Handle();
}