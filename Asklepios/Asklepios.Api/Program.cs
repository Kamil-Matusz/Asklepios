using Asklepios.Application;
using Asklepios.Core;
using Asklepios.Infrastructure;
using Asklepios.Infrastructure.Logging;
using Azure.Identity;
using Convey;
using Convey.MessageBrokers.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

var keyVaultEndpoint = new Uri(builder.Configuration["KeyVault:VaultUri"]);

builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

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