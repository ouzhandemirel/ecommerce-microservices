namespace StockService.Application.DTOs;

public class IncreaseStockCommand
{
    public Guid ProductId { get; }
    public int Quantity { get; }

    public IncreaseStockCommand(Guid productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }
}