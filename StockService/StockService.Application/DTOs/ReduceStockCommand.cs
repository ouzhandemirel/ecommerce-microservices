namespace StockService.Application.DTOs;

public class ReduceStockCommand
{
    public Guid ProductId { get; }
    public int Quantity { get; }

    public ReduceStockCommand(Guid productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }
}