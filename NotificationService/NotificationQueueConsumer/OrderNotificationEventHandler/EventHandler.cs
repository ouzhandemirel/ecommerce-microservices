using System;
using System.Text;
using DotNetCore.CAP;
using NotificationService.Application.Interfaces.Notifications;
using NotificationService.Application.Interfaces.UOW;
using NotificationService.Domain.Entities;
using Shared.Application.Exceptions;

namespace NotificationQueueConsumer.OrderNotificationEventHandler;

public class EventHandler : ICapSubscribe, IEventHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISmsNotifier _smsNotifier;
    private readonly IEmailNotifier _emailNotifier;

    public EventHandler(IUnitOfWork unitOfWork, ISmsNotifier smsNotifier, IEmailNotifier emailNotifier)
    {
        _unitOfWork = unitOfWork;
        _smsNotifier = smsNotifier;
        _emailNotifier = emailNotifier;
    }

    [CapSubscribe("order.notifications.queue")]
    public async Task Handle(OrderNotificationEvent orderNotificationEvent)
    {
        await _unitOfWork.BeginTransactionAsync();

        // Checking for idempotency
        if (await _unitOfWork.NotificationRepository.AnyAsync(x => x.Id == orderNotificationEvent.Id))
        {
            return;
        }
        var products = await _unitOfWork.ProductRepository.GetListAsync(x => orderNotificationEvent.OrderItems.Select(x => x.ProductId).Contains(x.Id));
        var customer = await _unitOfWork.CustomerRepository.GetAsync(x => x.Id == orderNotificationEvent.CustomerId)
            ?? throw new BusinessException("Customer not found.");

        var notification = new Notification(
            orderNotificationEvent.CustomerId,
            orderNotificationEvent.IsCancelled ? "Your order has been cancelled." : "Your order has been placed.",
            SetNotificationContent(orderNotificationEvent, products));

        notification.MarkAsSent();

        await _unitOfWork.NotificationRepository.AddAsync(notification);
        await _unitOfWork.CommitTransactionAsync();
    }

    private string SetNotificationContent(OrderNotificationEvent orderNotificationEvent, ICollection<Product> products)
    {
        var notificationContent = new StringBuilder();

        notificationContent.AppendLine("Order Details:");
        notificationContent.AppendLine($"Order Id: {orderNotificationEvent.OrderId}");
        notificationContent.AppendLine("Order Items:");

        foreach (var item in orderNotificationEvent.OrderItems)
        {
            var product = products.Single(x => x.Id == item.ProductId);
            notificationContent.AppendLine($"Product: {product.Name}");
            notificationContent.AppendLine($"Quantity: {item.Quantity}");
            notificationContent.AppendLine();
        }

        return notificationContent.ToString();    
    }
}
