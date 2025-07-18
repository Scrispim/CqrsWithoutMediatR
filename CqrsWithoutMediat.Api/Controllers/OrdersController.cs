using CqrsWithoutMediat.Api.Commands;
using CqrsWithoutMediat.Api.Entities.Dto;
using CqrsWithoutMediat.Api.Interfaces;
using CqrsWithoutMediat.Api.Query;
using Microsoft.AspNetCore.Mvc;

namespace CqrsWithoutMediat.Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly ICommandHandler<CreateOrderCommand> _handler;

    public OrdersController(ICommandHandler<CreateOrderCommand> handler)
    {
        _handler = handler;
    }

    [HttpPost()]
    public async Task<IActionResult> Create(CreateOrderCommand command)
    {
        await _handler.HandleAsync(command);
        return Ok("Pedido criado com sucesso");
    }
    
    [HttpGet()]
    public async Task<IActionResult> GetAll([FromServices] IQueryHandler<GetAllOrdersQuery, List<OrdersDto>> queryHandler)
    {
        var query = new GetAllOrdersQuery();
        var orders = await queryHandler.HandleAsync(query);
        return Ok(orders);
    }
}