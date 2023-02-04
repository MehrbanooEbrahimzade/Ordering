using Order.Api.Services;

namespace Order.Api.HostedServices;

public class HostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public HostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {

        for (var i = 1;!cancellationToken.IsCancellationRequested; i++)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                
                await orderService.Post(new OrderCommand
                {
                    ProductId = i * 1111,
                    Amount = i,
                    UserId = Guid.NewGuid(),
                    UserName = "customer" + i,
                    Number = $"{i}{i}{i}{i}",
                    EmailAddress = "customer" + i + "@gmail.com"
                });

                await Task.Delay(new TimeSpan(0, 0, 1), cancellationToken);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}