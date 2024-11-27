using Microsoft.AspNetCore.Mvc;
using StockService.Application.DTOs;
using StockService.Application.Interfaces.Services;

namespace StockService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly IStockService _stockService;
    
    public StockController(IStockService stockService)
    {
        _stockService = stockService;
    }
    
    [HttpGet("{productId:guid}")]
    public async Task<IActionResult> GetStock(Guid productId, CancellationToken cancellationToken)
    {
        var stock = await _stockService.GetStock(productId, cancellationToken);
        return Ok(stock);
    }
    
    [HttpPost("increase")]
    public async Task<IActionResult> IncreaseStock(IncreaseStockCommand command, CancellationToken cancellationToken)
    {
        await _stockService.IncreaseStock(command, cancellationToken);
        return Ok();
    }
    
    [HttpPost("reduce")]
    public async Task<IActionResult> ReduceStock(ReduceStockCommand command, CancellationToken cancellationToken)
    {
        await _stockService.ReduceStock(command, cancellationToken);
        return Ok();
    }
}