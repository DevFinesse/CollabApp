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

// explicit mapping: (User, roles) -> UserResponse
TypeAdapterConfig<(CollabApp.Domain.Entities.User user, IEnumerable<string> roles), CollabApp.Shared.Dtos.User.UserResponse>
    .NewConfig()
    .Map(dest => dest.Id, src => src.user.Id)
  .Map(dest => dest.FirstName, src => src.user.FirstName)
 .Map(dest => dest.LastName, src => src.user.LastName)
    .Map(dest => dest.Email, src => src.user.Email!)
    .Map(dest => dest.Status, src => src.user.Status)
    .Map(dest => dest.IsDisabled, src => src.user.IsDisabled)
    .Map(dest => dest.Roles, src => src.roles);

// explicit mapping: CreateUserRequest -> User
TypeAdapterConfig<CollabApp.Shared.Dtos.User.CreateUserRequest, CollabApp.Domain.Entities.User>
    .NewConfig()
    .Map(dest => dest.FirstName, src => src.FirstName)
    .Map(dest => dest.LastName, src => src.LastName)
    .Map(dest => dest.UserName, src => src.UserName)
    .Map(dest => dest.Email, src => src.Email)
    .Ignore(dest => dest.Id)
    .Ignore(dest => dest.SecurityStamp)
    .Ignore(dest => dest.Status)
    .Ignore(dest => dest.IsDisabled)
    .Ignore(dest => dest.CreatedAt)
 .Ignore(dest => dest.Members)
    .Ignore(dest => dest.Chats)
    .Ignore(dest => dest.Messages)
    .Ignore(dest => dest.RefreshTokens);

builder.Services.AddSingleton<IMapper>(new Mapper(mappingConfig));

builder.Services
    .AddFluentValidationAutoValidation()
    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

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
