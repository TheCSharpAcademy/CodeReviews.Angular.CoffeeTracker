using CoffeeTracker.UgniusFalze.Models;
using CoffeeTrackerAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

app.UseHttpsRedirection();

app.MapControllers();

app.Run();