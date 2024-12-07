using Microsoft.EntityFrameworkCore;
using ProductProject.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ProductsContext>(options =>
{
    string? connectionstring = builder.Configuration.GetValue<string>("CONNECTION_STRING");
    Console.WriteLine(connectionstring);
    if (connectionstring != null)
    {
        options.UseNpgsql(connectionstring);
    }
    else
    {
        Console.WriteLine("using harcoded: Host=database:5432;Database=products;Username=mehrdad;Password=1234");
        options.UseNpgsql("Host=database:5432;Database=products;Username=mehrdad;Password=1234");
    }
});
builder.Services.AddScoped<ProductRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:8200", "http://localhost:4200")  // Allow specific origins
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductsContext>();
    dbContext.Database.Migrate();
}
app.UseSwagger();
app.UseSwaggerUI();


// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
