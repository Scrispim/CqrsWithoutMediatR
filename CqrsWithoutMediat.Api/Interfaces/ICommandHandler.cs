namespace CqrsWithoutMediat.Api.Interfaces;

public interface ICommandHandler<in TCommand>
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}