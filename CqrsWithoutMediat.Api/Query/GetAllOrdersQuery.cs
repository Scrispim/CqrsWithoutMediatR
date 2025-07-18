using CqrsWithoutMediat.Api.Entities.Dto;

namespace CqrsWithoutMediat.Api.Query;

public class GetAllOrdersQuery
{
    public IEnumerable<OrdersDto> Orders { get; set; }
}