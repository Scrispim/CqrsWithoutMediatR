namespace CqrsWithoutMediat.Api.Commands;

public class CreateOrderCommand
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
}