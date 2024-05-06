using CoffeeTrackerAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var MyAllowOrigins = "_myAllowOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowOrigins,
    policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<CoffeeContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(MyAllowOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
