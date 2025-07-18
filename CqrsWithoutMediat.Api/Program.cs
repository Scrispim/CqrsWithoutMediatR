using CqrsWithoutMediat.Api.Commands;
using CqrsWithoutMediat.Api.Commands.Validators;
using CqrsWithoutMediat.Api.Entities.Dto;
using CqrsWithoutMediat.Api.Infrastructure.Decorators;
using CqrsWithoutMediat.Api.Interfaces;
using CqrsWithoutMediat.Api.Query;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar handlers
//builder.Services.AddScoped<ICommandHandler<CreateOrderCommand>, CreateOrderCommandHandler>();
//builder.Services.AddScoped<IQueryHandler<GetAllOrdersQuery, List<OrdersDto>>, GetAllOrdersQueryHandler>();

// Add controllers
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderCommandValidator>();


// Registra todos os handlers automaticamente
builder.Services.Scan(scan => scan
    .FromAssemblyOf<ICommandHandler<CreateOrderCommand>>() // qualquer tipo base
    .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
    .AsImplementedInterfaces()
    .WithScopedLifetime()
    .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);

// Adiciona os decoradores
builder.Services.Decorate(typeof(ICommandHandler<>), typeof(CommandHandlerValidationDecorator<>));
builder.Services.Decorate(typeof(ICommandHandler<>), typeof(CommandHandlerLoggingDecorator<>));
builder.Services.Decorate(typeof(IQueryHandler<,>), typeof(QueryHandlerLoggingDecorator<,>));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
