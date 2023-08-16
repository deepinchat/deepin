using DeepIn.Service.Common.Extensions; 
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.AddServiceDefaults();

var app = builder.Build();

app.UseServiceDefaults();

app.MapControllers();

app.Run();
