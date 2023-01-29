using Email.Api.EventBusConsumer;
using MassTransit;
using System.Reflection;
using Email.Application.Contracts.Persistence;
using FluentValidation;
using MediatR;
using Email.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add builder.Services to the container.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(Assembly.GetExecutingAssembly()); 
builder.Services.AddScoped<IEmailRepository, EmailRepository>();

// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SubmitOrderConsumer>(typeof(SubmitOrderConsumerDefinition));

    x.SetKebabCaseEndpointNameFormatter();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(configuration["EventBusSettings:HostAddress"]);
        cfg.ConfigureEndpoints(context);
    });
});

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
