using DeepIn.Emailing.API;
using DeepIn.Emailing.API.Infrastructure;
using DeepIn.Service.Common.Extensions; 

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults(eventHandlerAssembly: typeof(Program).Assembly);
builder.Services.AddEmailingServices(builder.Configuration);
var app = builder.Build();

app.UseServiceDefaults();
app.MigrateDbContext<EmailingDbContext>((_, __) => { });
app.Run();
