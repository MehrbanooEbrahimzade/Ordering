using EventBus.Messages.Common;
using MassTransit;
using Sms.Api.Consumer;
using EventBus.Messages.Events;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SmsOperationFinishedConsumer>().Endpoint(e =>
    {
        e.Temporary = true;
    });
    x.AddConsumer<SmsOrderSavedConsumer>();

    x.SetKebabCaseEndpointNameFormatter();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(configuration["EventBusSettings:HostAddress"]);
        cfg.ConfigureEndpoints(context);
        cfg.ReceiveEndpoint(EventBusConstants.OperationFinishedQueue, c =>
        {
            c.ConfigureConsumer<SmsOperationFinishedConsumer>(context);
        });
        cfg.ReceiveEndpoint(c =>
        {
            c.ConfigureConsumer<SmsOrderSavedConsumer>(context);
        });
    });
    
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();