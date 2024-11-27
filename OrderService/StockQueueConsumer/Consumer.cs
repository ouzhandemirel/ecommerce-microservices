using System;

namespace StockQueueConsumer;

public class Consumer : BackgroundService
{

    public Consumer() { }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}