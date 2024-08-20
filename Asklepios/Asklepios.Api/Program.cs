using Asklepios.Application;
using Asklepios.Core;
using Asklepios.Infrastructure;
using Asklepios.Infrastructure.Logging;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("Properties/secret.json", optional: true, reloadOnChange: true);

builder.Services
    .AddCore()
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

builder.UseSerilog();

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseInfrastructure();

app.MapControllers();

app.Run();