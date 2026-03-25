namespace Application.Abstractions;

public interface IUseCase< in TRequest, TResponse>
{
    Task<TResponse> Handle(TRequest request);
}