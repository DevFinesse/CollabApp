var builder = DistributedApplication.CreateBuilder(args);

// 
var redis = builder.AddRedis("redis")
    .WithDataVolume()
    .WithImageTag("7.4")
    .WithLifetime(ContainerLifetime.Persistent);

//
var sql = builder.AddSqlServer("sqlserver", port: 1433)
    .WithDataVolume()
    .AddDatabase("collabodb");

// Main API
builder.AddProject<Projects.CollabApp>("collabapp")
    .WithReference(sql)
    .WithReference(redis)
    .WithExternalHttpEndpoints();

builder.Build().Run();
