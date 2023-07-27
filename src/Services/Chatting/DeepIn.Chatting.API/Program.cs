using DeepIn.Service.Common.Extensions;
using DeepIn.Chatting.Application;
using DeepIn.Chatting.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults(eventHandlerAssembly: typeof(Program).Assembly);
builder.Services.AddChattingInfrastructure(builder.Configuration);
builder.Services.AddChattingApplication(builder.Configuration);
builder.Services.AddControllers(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseServiceDefaults();

app.MapControllers();

app.Run();
