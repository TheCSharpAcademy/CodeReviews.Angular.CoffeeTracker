using Asp.Versioning.Conventions;
using CoffeeTracker.Api.Contracts.V1;
using CoffeeTracker.Api.Filters;
using CoffeeTracker.Api.Models;
using CoffeeTracker.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeTracker.Api.Routes;

/// <summary>
/// Defines the API endpoints for managing coffee records. 
/// It provides methods to map routes for creating, retrieving, updating, and deleting coffee records, 
/// and supports versioned APIs using Asp.Versioning.
/// </summary>
public static class CoffeeTracker
{
    #region Methods: Public

    public static WebApplication MapCoffeeTrackerEndpoints(this WebApplication app)
    {
        var apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(1)
            .HasApiVersion(2)
            .ReportApiVersions()
            .Build();

        var builder = app.MapGroup("/api/v{version:apiVersion}/coffees")
            .WithApiVersionSet(apiVersionSet);

        builder.MapGet("/", GetAllCoffees)
            .WithName(nameof(GetAllCoffees))
            .WithSummary("Get all coffee records.")
            .MapToApiVersion(1);

        builder.MapGet("/{id}", GetCoffee)
            .WithName(nameof(GetCoffee))
            .WithSummary("Get a coffee record by ID.")
            .MapToApiVersion(1);

        builder.MapPost("/", CreateCoffee)
            .WithName(nameof(CreateCoffee))
            .WithSummary("Create a new coffee record.")
            .MapToApiVersion(1)
            .WithRequestValidation<CoffeeRecordRequest>();

        builder.MapPut("/{id}", UpdateCoffee)
            .WithName(nameof(UpdateCoffee))
            .WithSummary("Update an existing coffee record.")
            .MapToApiVersion(1)
            .WithRequestValidation<CoffeeRecordRequest>();

        builder.MapDelete("/{id}", DeleteCoffee)
            .WithName(nameof(DeleteCoffee))
            .WithSummary("Delete an existing coffee record.")
            .MapToApiVersion(1);

        return app;
    }

    #endregion
    #region Methods: Private

    private static async Task<IResult> CreateCoffee([FromBody] CoffeeRecordRequest request, [FromServices] ICoffeeRecordService service)
    {
        var model = request.ToModel();

        var created = await service.CreateAsync(model);

        return created
        ? TypedResults.CreatedAtRoute(model.ToResponse(), nameof(GetCoffee), new { id = model.Id })
        : TypedResults.BadRequest(new { error = "Unable to create coffee." });
    }

    private static async Task<IResult> DeleteCoffee([FromRoute] Guid id, [FromServices] ICoffeeRecordService service)
    {
        var entity = await service.ReturnAsync(id);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        var deleted = await service.DeleteAsync(entity);

        return deleted
            ? TypedResults.NoContent()
            : TypedResults.BadRequest(new { error = "Unable to delete coffee." });
    }

    private static async Task<IResult> GetAllCoffees([FromServices] ICoffeeRecordService service)
    {
        var entities = await service.ReturnAsync();
        return TypedResults.Ok(entities.Select(x => x.ToResponse()));
    }

    private static async Task<IResult> GetCoffee([FromRoute] Guid id, [FromServices] ICoffeeRecordService service)
    {
        var entity = await service.ReturnAsync(id);

        return entity is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(entity.ToResponse());
    }

    private static async Task<IResult> UpdateCoffee([FromRoute] Guid id, [FromBody] CoffeeRecordRequest request, [FromServices] ICoffeeRecordService service)
    {
        var entity = await service.ReturnAsync(id);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        var updatedCoffeeRecord = new CoffeeRecord
        {
            Id = entity.Id,
            Name = request.Name,
            Date = request.Date,
        };

        var updated = await service.UpdateAsync(updatedCoffeeRecord);

        return updated
            ? TypedResults.Ok(updatedCoffeeRecord.ToResponse())
            : TypedResults.BadRequest(new { error = "Unable to update coffee." });
    }

    #endregion
}
