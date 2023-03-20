var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();

app.Run();
