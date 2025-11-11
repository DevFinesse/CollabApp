using CollabApp.Application.Users.Commands.Handlers;
using CollabApp.Infrastructure.Persistence;
using CollabApp.Extensions;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System;
using System.Reflection;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterApplicationServices(builder.Configuration);
builder.AddRedisClient("redis");

// Configure Mapster
var mappingConfig = TypeAdapterConfig.GlobalSettings;
mappingConfig.Scan(Assembly.GetExecutingAssembly());
mappingConfig.Scan(typeof(CollabApp.Application.Users.Commands.Models.CreateUserCommand).Assembly);

builder.Services.AddSingleton<IMapper>(new Mapper(mappingConfig));

// Register validators from both Web and Application assemblies
builder.Services
    .AddFluentValidationAutoValidation()
    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
    .AddValidatorsFromAssembly(typeof(CollabApp.Application.Users.Commands.Models.CreateUserCommand).Assembly);

builder.Services.AddMediatR(cfg => 
{
    cfg.RegisterServicesFromAssemblies(
        Assembly.GetExecutingAssembly(),
        typeof(CollabApp.Application.Users.Commands.Models.CreateUserCommand).Assembly,
        typeof(CollabApp.Infrastructure.Persistence.ApplicationDbContext).Assembly
    );
});

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
 app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
