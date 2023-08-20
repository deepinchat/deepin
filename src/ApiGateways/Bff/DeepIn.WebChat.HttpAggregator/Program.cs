using DeepIn.Service.Common.Extensions;
using DeepIn.WebChat.HttpAggregator.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.AddServiceDefaults();
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.UseServiceDefaults();

app.MapControllers();

app.Run();
