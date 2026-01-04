using Asklepios.Application;
using Asklepios.Core;
using Asklepios.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

builder.Services
    .AddCore()
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseInfrastructure();

app.MapControllers();

app.Run();