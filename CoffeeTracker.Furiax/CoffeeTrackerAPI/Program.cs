using CoffeeTrackerAPI.Data;
using Microsoft.EntityFrameworkCore;

var MyAllowOrigins = "_myAllowOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<CoffeeContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("CoffeeTracker")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowOrigins,
    policy =>
    {
        policy.AllowAnyOrigin()
        //.WithOrigins("http://127.0.0.1:4200/")
        .AllowAnyMethod()
        .AllowAnyHeader();
        //.AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(MyAllowOrigins);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}
app.UseHttpsRedirection();
app.UseCors(MyAllowOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
