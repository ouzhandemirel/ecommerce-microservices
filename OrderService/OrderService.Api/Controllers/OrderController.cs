using Microsoft.AspNetCore.Mvc;
using OrderService.Application.DTOs;
using OrderService.Application.Interfaces.Services;

namespace OrderService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderServce;

    public OrderController(IOrderService orderService)
    {
        _orderServce = orderService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        await _orderServce.CreateOrder(command, cancellationToken);
        return Ok();
    }

    [HttpPost("cancel")]
    public async Task<IActionResult> CancelOrder(CancelOrderCommand command, CancellationToken cancellationToken)
    {
        await _orderServce.CancelOrder(command, cancellationToken);
        return Ok();
    }

}