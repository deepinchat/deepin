using DeepIn.Service.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults(eventHandlerAssembly: typeof(Program).Assembly);
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseServiceDefaults();

app.MapControllers();

app.Run();
