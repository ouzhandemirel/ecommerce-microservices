using System;

namespace OrderService.Application.DTOs;

public class CancelOrderCommand
{
    public Guid OrderId { get; set; }
}
