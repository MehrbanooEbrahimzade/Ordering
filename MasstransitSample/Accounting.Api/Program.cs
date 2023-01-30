using Accounting.Api.Repository;
using Accounting.Api.Repository.IRepository;
using Accounting.Api.RequestConsumer;
using EventBus.Messages.Events;
using MassTransit;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddSingleton<IAccountingRepository, AccountingRepository>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CheckOrderStatusConsumer>();

    x.SetKebabCaseEndpointNameFormatter();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(configuration["EventBusSettings:HostAddress"]);
        cfg.ConfigureEndpoints(context);
    });
    x.AddRequestClient<OrderSubmittedResponse>();
});
builder.Services.AddScoped<CheckOrderStatusConsumer>();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();