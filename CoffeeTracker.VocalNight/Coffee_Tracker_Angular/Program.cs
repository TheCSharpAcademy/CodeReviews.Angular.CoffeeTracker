using Coffee_Tracker_Angular.Database;
using Coffee_Tracker_Angular.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CoffeeContext>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("Database")));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = scope.ServiceProvider.GetRequiredService<CoffeeContext>();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.MapPost("/tracker", async ( CoffeeRecords item, CoffeeContext db ) =>
{
    db.CoffeeRecords.Add(item);
    await db.SaveChangesAsync();

    return (Results.Created($"/tracker/{item.Id}", item));
});

app.MapGet("/tracker", async ( CoffeeContext db ) =>
    await db.CoffeeRecords.ToListAsync());

app.MapDelete("/tracker/{id}", async ( int id, CoffeeContext db ) =>
{
    if (await db.CoffeeRecords.FindAsync(id) is CoffeeRecords item)
    {
        db.CoffeeRecords.Remove(item);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});




app.UseCors("AllowAngularDev");
app.Run();

