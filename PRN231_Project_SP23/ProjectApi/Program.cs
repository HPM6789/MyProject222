var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddAutoMapper(typeof(BusinessObjects.DTO.MyMapperProfile));
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
