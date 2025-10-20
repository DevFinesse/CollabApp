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

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterApplicationServices(builder.Configuration);
builder.AddRedisClient("redis");
var mappingConfig = TypeAdapterConfig.GlobalSettings;
// Scan main + Application assembly for Mapster configs
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
    .Ignore(dest => dest.Id) // Let Identity handle this
    .Ignore(dest => dest.SecurityStamp) // Let Identity handle this
    .Ignore(dest => dest.Status) // Use default value
    .Ignore(dest => dest.IsDisabled) // Use default value
    .Ignore(dest => dest.CreatedAt) // Use default value
    .Ignore(dest => dest.Members) // Ignore collections
    .Ignore(dest => dest.Chats) // Ignore collections
    .Ignore(dest => dest.Messages) // Ignore collections
    .Ignore(dest => dest.RefreshTokens); // Ignore collections
builder.Services.AddSingleton<IMapper>(new Mapper(mappingConfig));

builder.Services
    .AddFluentValidationAutoValidation()
    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(med => med.RegisterServicesFromAssemblies(
    Assembly.GetExecutingAssembly(),
    typeof(CollabApp.Application.Users.Commands.Models.CreateUserCommand).Assembly,
    typeof(CollabApp.Infrastructure.Persistence.ApplicationDbContext).Assembly));


var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
