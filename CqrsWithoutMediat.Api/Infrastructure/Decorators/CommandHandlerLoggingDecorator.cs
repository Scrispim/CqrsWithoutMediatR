using CqrsWithoutMediat.Api.Interfaces;

namespace CqrsWithoutMediat.Api.Infrastructure.Decorators;

public class CommandHandlerLoggingDecorator<TCommand> : ICommandHandler<TCommand>
{
    private readonly ICommandHandler<TCommand> _inner;
    private readonly ILogger<CommandHandlerLoggingDecorator<TCommand>> _logger;

    public CommandHandlerLoggingDecorator(
        ICommandHandler<TCommand> inner,
        ILogger<CommandHandlerLoggingDecorator<TCommand>> logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public async Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Executando comando: {CommandType}", typeof(TCommand).Name);
        await _inner.HandleAsync(command, cancellationToken);
        _logger.LogInformation("Comando finalizado: {CommandType}", typeof(TCommand).Name);
    }
}
