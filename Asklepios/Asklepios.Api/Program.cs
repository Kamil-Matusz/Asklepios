using Asklepios.Application;
using Asklepios.Core;
using Asklepios.Infrastructure;
using Asklepios.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.UseSerilog();

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseInfrastructure();

app.MapControllers();

app.Run();