var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices()
    .AddOptions(builder.Configuration)
    .AddSerialization()
    .AddDatabase(builder.Configuration)
    .AddApiHttpClient(builder.Configuration)
    .RegisterSwagger();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.MapControllers();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.Run();

public partial class Program { }