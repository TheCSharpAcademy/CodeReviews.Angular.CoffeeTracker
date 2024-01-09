using kmakai.CoffeeTrackerAPI.Angular;
using kmakai.CoffeeTrackerAPI.Angular.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<TrackerContext>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("TrackerConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/coffees", async (TrackerContext db) =>
{
    var coffees = await db.Coffees.ToListAsync();
    return await db.Coffees.ToListAsync();
});

app.MapPost("/coffees", async (TrackerContext db, Coffee coffee) =>
{
    var coffees = await db.Coffees.ToListAsync();

    if (coffees.Any(c => c.Date.Date == coffee.Date.Date))
    {
        var existingCoffee = coffees.First(c => c.Date.Date == coffee.Date.Date);
        existingCoffee.Cups += coffee.Cups;
        await db.SaveChangesAsync();
        return Results.Created($"/coffees/{existingCoffee.Id}", existingCoffee);
    }
    else
    {
        await db.Coffees.AddAsync(coffee);
        await db.SaveChangesAsync();
        return Results.Created($"/coffees/{coffee.Id}", coffee);
    }

});

app.MapGet("/coffees/{id}", async (int id, TrackerContext db) =>

     await db.Coffees.FindAsync(id) is Coffee coffee ? Results.Ok(coffee) : Results.NotFound()
);

app.MapPut("/coffees/{id}", async (int id, Coffee update, TrackerContext db) =>
{
    var coffee = await db.Coffees.FindAsync(id);
    if (coffee is null)
    {
        return Results.NotFound();
    }

    coffee.Date = update.Date;
    coffee.Cups = update.Cups;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/coffees/{id}", async (int id, TrackerContext db) =>
{
    var coffee = await db.Coffees.FindAsync(id);
    if (coffee is null)
    {
        return Results.NotFound();
    }

    db.Coffees.Remove(coffee);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.UseCors("AllowAll");

app.Run();

