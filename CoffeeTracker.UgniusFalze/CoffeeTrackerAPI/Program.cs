using CoffeeTracker.UgniusFalze.Models;
using CoffeeTrackerAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options => options.AddPolicy(name: "CoffeeTrackerUI",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CoffeeRecordContext>(opt =>
    opt.UseSqlServer(
        "Server=(LocalDb)\\MSSQLLocalDB;Initial Catalog=CoffeeRecords;Integrated Security=SSPI;Trusted_Connection=yes"));
builder.Services.AddScoped<ICoffeeRepository, CoffeeRepository>();
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CoffeeTrackerUI");
app.UseHttpsRedirection();


app.MapControllers();

app.Run();