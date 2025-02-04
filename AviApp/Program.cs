using System.Reflection;
using AviApp.Domain.Context;
using AviApp.Errors.ProblemFactory;
using AviApp.Interfaces;
using AviApp.Pipelines;
using AviApp.Services;
using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddProblemDetails();
builder.Services.AddDbContext<AvipAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<ProblemDetailsFactory, ProblemFactory>();

var app = builder.Build();
app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManager API V1");
    });
}

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();