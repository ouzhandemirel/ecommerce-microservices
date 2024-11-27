using Shared.Domain.Exceptions;
using StockService.Application.DTOs;
using StockService.Application.Interfaces.Services;
using StockService.Application.Interfaces.UOW;

namespace StockService.Application.Services;

public class StockService : IStockService
{
    private readonly IUnitOfWork _unitOfWork;
    
    public StockService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> GetStock(Guid productId, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetAsync(x => x.Id == productId, cancellationToken: cancellationToken) 
                      ?? throw new Exception("Product not found");
        return product.Quantity;
    }
    
    public async Task IncreaseStock(IncreaseStockCommand command, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        var product = await _unitOfWork.ProductRepository.GetAsync(x => x.Id == command.ProductId, cancellationToken: cancellationToken) 
                      ?? throw new DomainException("Product not found");
        product.IncreaseStock(command.Quantity);
        await _unitOfWork.ProductRepository.UpdateAsync(product);
        await _unitOfWork.CommitTransactionAsync(cancellationToken);
    }
    
    public async Task ReduceStock(ReduceStockCommand command, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        var product = await _unitOfWork.ProductRepository.GetAsync(x => x.Id == command.ProductId, cancellationToken: cancellationToken)
                      ?? throw new Exception("Product not found");
        product.ReduceStock(command.Quantity);
        await _unitOfWork.ProductRepository.UpdateAsync(product);
        await _unitOfWork.CommitTransactionAsync(cancellationToken);
    }
}