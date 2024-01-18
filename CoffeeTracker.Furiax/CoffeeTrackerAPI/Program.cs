using CoffeeTrackerAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<CoffeeContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("CoffeeTracker")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(); //add CORS so access from angular is possible

var app = builder.Build();

app.UseCors(options =>
    options.WithOrigins("http://localhost:5270")
    .AllowAnyMethod()
    .AllowAnyHeader()); // configure CORS so AngularMethods can make calls

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
