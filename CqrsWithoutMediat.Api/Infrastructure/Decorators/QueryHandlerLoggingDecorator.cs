using CqrsWithoutMediat.Api.Interfaces;

namespace CqrsWithoutMediat.Api.Infrastructure.Decorators;

public class QueryHandlerLoggingDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
{
    private readonly IQueryHandler<TQuery, TResult> _inner;
    private readonly ILogger<QueryHandlerLoggingDecorator<TQuery, TResult>> _logger;

    public QueryHandlerLoggingDecorator(
        IQueryHandler<TQuery, TResult> inner,
        ILogger<QueryHandlerLoggingDecorator<TQuery, TResult>> logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public async Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Executando consulta: {QueryType}", typeof(TQuery).Name);
        var result = await _inner.HandleAsync(query, cancellationToken);
        _logger.LogInformation("Consulta finalizada: {QueryType}", typeof(TQuery).Name);
        return result;
    }
}
