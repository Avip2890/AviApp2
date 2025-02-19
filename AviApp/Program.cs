using System.Reflection;
using System.Security.Claims;
using System.Text;
using AviApp.Domain.Context;
using AviApp.Errors.ProblemFactory;
using AviApp.Interfaces;
using AviApp.Pipelines;
using AviApp.Services;
using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<JwtService>(sp => 
    new JwtService(builder.Configuration["JwtSettings:SecretKey"] ?? throw new ArgumentNullException("JwtSettings:SecretKey", "Secret key is missing"))
);

var jwtSecret = builder.Configuration["JwtSettings:SecretKey"] 
                ?? throw new ArgumentNullException("JwtSettings:SecretKey", "Secret key is missing");

builder.Services.AddAuthentication(options => {  
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            RoleClaimType = ClaimTypes.Role
        };
    });


builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddProblemDetails();
builder.Services.AddDbContext<AvipAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthorization();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Define the Bearer Token security scheme
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Description = "Please enter 'Bearer' followed by your token",
        Scheme = "bearer",
    });

    // Apply security globally to all endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            []
        }
    });
});
builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<ProblemDetailsFactory, ProblemFactory>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManager API V1");
});

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
