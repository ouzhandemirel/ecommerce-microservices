using System;

namespace OrderService.Application.DTOs;

public class CreateOrderCommand
{
    public Guid CustomerId { get; }
    public List<OrderLineItem> OrderLineItems { get; }

    public CreateOrderCommand(Guid customerId, List<OrderLineItem> orderLineItems)
    {
        CustomerId = customerId;
        OrderLineItems = orderLineItems;
    }

    public class OrderLineItem
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
