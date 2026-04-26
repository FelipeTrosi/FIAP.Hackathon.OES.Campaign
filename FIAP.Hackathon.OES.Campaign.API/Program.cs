using FIAP.Hackathon.OES.Campaign.API.Extensions;
using FIAP.Hackathon.OES.User.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Prometheus;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
           .AddEnvironmentVariables()
           .Build();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FIAP",
        Version = "v1",
        Description = "API criada para validação dos entregaveis do curso POS-TECH Arquitetura de Sistemas .NET com Azure"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Digite o token JWT sem 'Bearer '"
    });

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
            Array.Empty<string>()
        }
    });
});

#endregion

#region Data

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Scoped);

#endregion

#region JWT

builder.Services.UseJwtAuthentication(builder.Configuration);

#endregion

#region DI

builder.Services.AddCorrelationIdGenerator();
builder.Services.AddRepositoryDI();
builder.Services.AddServiceDI();

#endregion

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddHealthChecks();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Middleware

app.UseCorrelationMiddleware();
app.UseLogMiddleware();
app.UseGlobalExceptionHandler();

#endregion

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpMetrics();


app.MapMetrics();
app.MapControllers();
app.MapHealthChecks("/health");
app.MapGet("/health", () => Results.Ok("Healthy"));

app.Run();
