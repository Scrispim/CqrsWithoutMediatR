using CqrsWithoutMediat.Api.Interfaces;
using FluentValidation;

namespace CqrsWithoutMediat.Api.Infrastructure.Decorators;

public class CommandHandlerValidationDecorator<TCommand> : ICommandHandler<TCommand>
{
    private readonly ICommandHandler<TCommand> _inner;
    private readonly IEnumerable<IValidator<TCommand>> _validators;

    public CommandHandlerValidationDecorator(
        ICommandHandler<TCommand> inner,
        IEnumerable<IValidator<TCommand>> validators)
    {
        _inner = inner;
        _validators = validators;
    }

    public async Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        foreach (var validator in _validators)
        {
            var result = await validator.ValidateAsync(command, cancellationToken);
            if (!result.IsValid)
            {
                var errors = string.Join(" | ", result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Falha na validação de {typeof(TCommand).Name}: {errors}");
            }
        }

        await _inner.HandleAsync(command, cancellationToken);
    }
}
