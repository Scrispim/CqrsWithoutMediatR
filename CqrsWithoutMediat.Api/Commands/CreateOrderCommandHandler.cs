using CqrsWithoutMediat.Api.Interfaces;

namespace CqrsWithoutMediat.Api.Commands;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
{
    public Task HandleAsync(CreateOrderCommand command, CancellationToken cancellationToken = default)
    {
        // Simulando lógica de criação (sem banco real)
        Console.WriteLine($"Criando pedido: Produto = {command.ProductName}, Quantidade = {command.Quantity}");
        return Task.CompletedTask;
    }
}