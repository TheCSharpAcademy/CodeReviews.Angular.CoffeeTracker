using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeTracker.Models;
using System.Data;

namespace ShiftsLoggerWebApi.Controllers;

[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route("api/CoffeeCups")]
public class CoffeeCupsController(CoffeeTrackerContext dbContext) : ControllerBase
{
    private readonly CoffeeTrackerContext CoffeeContext = dbContext;

    [HttpGet]
    public async Task<IResult> GetCoffeeCups(string? date)
    {
        if (CoffeeContext.Coffee == null)
            return TypedResults.Problem("Entity set 'Coffee'  is null.");

        var query = from m in CoffeeContext.Coffee select m;
        query = query.OrderByDescending( p => p.Date);
        
        if( DateTime.TryParse( date, out DateTime dateResult))
            query = query.Where( p => p.Date!.Value.Date == dateResult);

        return TypedResults.Ok(await query.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetCoffeeCup( int id)
    {
        if (CoffeeContext.Coffee == null)
            return TypedResults.Problem("Entity set 'Coffee'  is null.");

        var cup = await CoffeeContext.Coffee.FindAsync(id);
        if(cup is null)
            return TypedResults.NotFound();

        return TypedResults.Ok( cup );
    }

    [HttpPost]
    [Consumes("application/json")]
    public async Task<IResult> PostCoffeeCup( [FromBody] CoffeeCups cup)
    {
        if (!ModelState.IsValid)
            return TypedResults.BadRequest();

        CoffeeContext.Coffee.Add(cup);
        try
        {
            await CoffeeContext.SaveChangesAsync();
        }
        catch
        {
            return TypedResults.StatusCode(500);
        }
        
        return TypedResults.Created($"/{cup.Id}",cup);
    }

    [HttpDelete("{id}")]
    [Consumes("application/json")]
    public async Task<IResult> DeleteCoffeeCup( int id)
    {
        var cup = await CoffeeContext.Coffee.FindAsync( id );
        if( cup is null)
            return TypedResults.NotFound();
        
        CoffeeContext.Coffee.Remove(cup);
        try
        {
            await CoffeeContext.SaveChangesAsync();
        }
        catch
        {
            return TypedResults.StatusCode(500);
        }

        return TypedResults.Ok();
    }

    [HttpPut("{id}")]
    [Consumes("application/json")]
    public async Task<IResult> PutCoffeeCup( int id, [FromBody] CoffeeCups cup )
    {
        if(!ModelState.IsValid || id != cup.Id)
            return TypedResults.BadRequest();

        if( !CoffeeContext.Coffee.Any( p => p.Id == id))
            return TypedResults.NotFound();

        CoffeeContext.Coffee.Update(cup);
        try
        {
            await CoffeeContext.SaveChangesAsync();
        }
        catch
        {
            return TypedResults.StatusCode(500);
        }

        return TypedResults.Ok(cup);
    }
}