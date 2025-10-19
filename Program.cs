using Hng0Task.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient<ProfileService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Disable HTTPS redirect (since Pxxl handles HTTPS)
// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
