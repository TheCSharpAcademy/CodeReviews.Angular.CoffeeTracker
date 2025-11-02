using CoffeeTracker.Api.Models;
using CoffeeTracker.Api.Data;
using Microsoft.EntityFrameworkCore;
using CoffeeTracker.Api.Responses;

namespace CoffeeTracker.Api.Repositories;

public class CoffeeRepository : ICoffeeRepository
{
    private readonly CoffeeTrackerDbContext _dbContext;

    public CoffeeRepository(CoffeeTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<List<Coffee>>> GetPagedCoffees(PaginationParams paginationParams)
    {
        var response = new PagedResponse<List<Coffee>>(data: new List<Coffee>(),
                                                       pageNumber: paginationParams.Page,
                                                       pageSize: paginationParams.PageSize,
                                                       totalRecords: 0);

        try
        {
            var query = _dbContext.Coffees.AsQueryable();

            if (!string.IsNullOrEmpty(paginationParams.Name))
                query = query.Where(p => p.Name.Contains(paginationParams.Name));
            if (paginationParams.MinPrice.HasValue)
                query = query.Where(p => p.Price >= paginationParams.MinPrice.Value);
            if (paginationParams.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= paginationParams.MaxPrice.Value);

            var totalRecords = await query.CountAsync();

            var sortBy = paginationParams.SortBy?.Trim().ToLower() ?? "id";
            var sortAscending = paginationParams.SortAscending;

            bool useAscending = sortAscending ?? (sortBy == "id" ? false : true);

            query = sortBy switch
            {
                "name" => useAscending ? query.OrderBy(c => c.Name) : query.OrderByDescending(c => c.Name),
                "price" => useAscending ? query.OrderBy(c => c.Price) : query.OrderByDescending(c => c.Price),
                _ => useAscending ? query.OrderBy(c => c.Id) : query.OrderByDescending(c => c.Id)
            };

            var pagedCoffees = await query.Skip((paginationParams.Page - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            response.Status = ResponseStatus.Success;
            response.Data = pagedCoffees;
            response.TotalRecords = totalRecords;
        }
        catch (Exception ex)
        {
            response.Message = $"Error in CoffeeRepository {nameof(GetPagedCoffees)}: {ex.Message}";
            response.Status = ResponseStatus.Fail;
        }

        return response;
    }

    public async Task<BaseResponse<Coffee>> GetCoffeeById(int id)
    {
        var response = new BaseResponse<Coffee>();

        try
        {
            var coffee = await _dbContext.Coffees.FirstOrDefaultAsync(c => c.Id == id);

            if (coffee == null)
            {
                response.Status = ResponseStatus.Fail;
                response.Message = "Coffee not found.";
            }
            else
            {
                response.Status = ResponseStatus.Success;
                response.Data = coffee;
            }
        }
        catch (Exception ex)
        {
            response.Message = $"Error in CoffeeRepository {nameof(CoffeeRepository)}: {ex.Message}";
            response.Status = ResponseStatus.Fail;
        }

        return response;
    }

    public async Task<BaseResponse<Coffee>> CreateCoffee(Coffee coffee)
    {
        var response = new BaseResponse<Coffee>();

        try
        {
            _dbContext.Coffees.Add(coffee);

            await _dbContext.SaveChangesAsync();

            if (coffee == null)
            {
                response.Status = ResponseStatus.Fail;
                response.Message = "Coffee not created.";
            }
            else
            {
                response.Status = ResponseStatus.Success;
                response.Data = coffee;
            }
        }
        catch (Exception ex)
        {
            response.Message = $"Error in CoffeeRepository {nameof(CreateCoffee)}: {ex.Message}";
            response.Status = ResponseStatus.Fail;
        }

        return response;
    }

    public async Task<BaseResponse<Coffee>> UpdateCoffee(Coffee updatedCoffee)
    {
        var response = new BaseResponse<Coffee>();

        try
        {
            _dbContext.Coffees.Update(updatedCoffee);
            var affectedRows = await _dbContext.SaveChangesAsync();

            if (affectedRows == 0)
            {
                response.Status = ResponseStatus.Fail;
                response.Message = "No changes were saved.";
            }
            else
            {
                response.Status = ResponseStatus.Success;
                response.Data = updatedCoffee;
            }
        }
        catch (Exception ex)
        {
            response.Message = $"Error in CoffeeRepository {nameof(UpdateCoffee)}: {ex.Message}";
            response.Status = ResponseStatus.Fail;
        }

        return response;
    }

    public async Task<BaseResponse<Coffee>> DeleteCoffee(int id)
    {
        var response = new BaseResponse<Coffee>();

        try
        {
            response = await GetCoffeeById(id);

            response.Data.IsDeleted = true;

            _dbContext.Coffees.Update(response.Data);
            var affectedRows = await _dbContext.SaveChangesAsync();

            if (affectedRows == 0)
            {
                response.Status = ResponseStatus.Fail;
                response.Message = "Deletion failed.";
            }
            else
            {
                response.Status = ResponseStatus.Success;
                response.Message = "Coffee deleted.";
            }
        }
        catch (Exception ex)
        {
            response.Message = $"Error in CoffeeRepository {nameof(DeleteCoffee)}: {ex.Message}";
            response.Status = ResponseStatus.Fail;
        }

        return response;
    }
}
