using CqrsWithoutMediat.Api.Entities.Dto;
using CqrsWithoutMediat.Api.Interfaces;

namespace CqrsWithoutMediat.Api.Query;

public class GetAllOrdersQueryHandler : IQueryHandler<GetAllOrdersQuery, List<OrdersDto>>
{
    public Task<List<OrdersDto>> HandleAsync(GetAllOrdersQuery query,
        CancellationToken cancellationToken = default)
    {
        var orders = new List<OrdersDto>
        {
            new OrdersDto { ProductName = "Product 1", Quantity = 10 },
            new OrdersDto { ProductName = "Product 2", Quantity = 20 },
            new OrdersDto { ProductName = "Product 3", Quantity = 30 }
        };
        
        return Task.FromResult(orders);
    }
}