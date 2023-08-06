using DeepIn.Messaging.API;
using DeepIn.Service.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults(eventHandlerAssembly: typeof(Program).Assembly);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddControllers();
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5009);
});
var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseServiceDefaults();

app.MapControllers();

app.Run();
