using CoffeeTracker.Api.Data;
using CoffeeTracker.Api.Models;
using CoffeeTracker.Api.Responses;
using Microsoft.EntityFrameworkCore;

namespace CoffeeTracker.Api.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly CoffeeTrackerDbContext _dbContext;

    public SaleRepository(CoffeeTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<List<Sale>>> GetPagedSales(PaginationParams paginationParams)
    {
        var response = new PagedResponse<List<Sale>>(data: new List<Sale>(),
                                                       pageNumber: paginationParams.Page,
                                                       pageSize: paginationParams.PageSize,
                                                       totalRecords: 0);

        try
        {
            var query = _dbContext.Sales.AsQueryable();

            if (paginationParams.MinPrice.HasValue)
                query = query.Where(s => s.Total >= paginationParams.MinPrice.Value);
            if (paginationParams.MaxPrice.HasValue)
                query = query.Where(s => s.Total <= paginationParams.MaxPrice.Value);
            if (paginationParams.MinDateOfSale.HasValue)
                query = query.Where(s => DateOnly.FromDateTime(s.DateAndTimeOfSale) >= paginationParams.MinDateOfSale.Value);
            if (paginationParams.MaxDateOfSale.HasValue)
                query = query.Where(s => DateOnly.FromDateTime(s.DateAndTimeOfSale) <= paginationParams.MaxDateOfSale.Value);

            var totalRecords = await query.CountAsync();

            var sortBy = paginationParams.SortBy?.Trim().ToLower() ?? "saleid";
            var sortAscending = paginationParams.SortAscending;

            bool useAscending = sortAscending ?? (sortBy == "saleid" ? false : true);

            query = sortBy switch
            {
                "total" => useAscending ? query.OrderBy(s => s.Total) : query.OrderByDescending(s => s.Total),
                "dateOfSale" => useAscending ? query.OrderBy(s => s.DateAndTimeOfSale) : query.OrderByDescending(s => s.DateAndTimeOfSale),
                _ => useAscending ? query.OrderBy(s => s.Id) : query.OrderByDescending(s => s.Id)
            };

            var pagedSales = await query
                                    .Skip((paginationParams.Page - 1) * paginationParams.PageSize)
                                    .Take(paginationParams.PageSize)
                                    .ToListAsync();

            response.Status = ResponseStatus.Success;
            response.Data = pagedSales;
            response.TotalRecords = totalRecords;
        }
        catch (Exception ex)
        {
            response.Message = $"Error in SaleRepository {nameof(GetPagedSales)}: {ex.Message}";
            response.Status = ResponseStatus.Fail;
        }

        return response;
    }

    public async Task<BaseResponse<Sale>> GetSaleById(int id)
    {
        var response = new BaseResponse<Sale>();

        try
        {
            var sale = await _dbContext.Sales.FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
            {
                response.Status = ResponseStatus.Fail;
                response.Message = "Sale not found.";
            }
            else
            {
                response.Status = ResponseStatus.Success;
                response.Data = sale;
            }
        }
        catch (Exception ex)
        {
            response.Message = $"Error in SaleRepository {nameof(GetSaleById)}: {ex.Message}";
            response.Status = ResponseStatus.Fail;
        }

        return response;
    }

    public async Task<BaseResponse<Sale>> CreateSale(Sale sale)
    {
        var response = new BaseResponse<Sale>();

        try
        {
            _dbContext.Sales.Add(sale);

            await _dbContext.SaveChangesAsync();

            if (sale == null)
            {
                response.Status = ResponseStatus.Fail;
                response.Message = "Sale not created.";
            }
            else
            {
                response.Status = ResponseStatus.Success;
                response.Data = sale;
            }
        }
        catch (Exception ex)
        {
            response.Message = $"Error in SaleRepository {nameof(CreateSale)}: {ex.Message}";
            response.Status = ResponseStatus.Fail;
        }

        return response;
    }

public async Task<BaseResponse<Sale>> UpdateSale(Sale updatedSale)
{
    var response = new BaseResponse<Sale>();

    try
    {
        _dbContext.Sales.Update(updatedSale);
        var affectedRows = await _dbContext.SaveChangesAsync();

        if (affectedRows == 0)
        {
            response.Status = ResponseStatus.Fail;
            response.Message = "No changes were saved.";
        }
        else
        {
            await _dbContext.Entry(updatedSale).Reference(s => s.Coffee).LoadAsync();

            response.Status = ResponseStatus.Success;
            response.Data = updatedSale;
        }
    }
    catch (Exception ex)
    {
        response.Message = $"Error in SaleRepository {nameof(UpdateSale)}: {ex.Message}";
        response.Status = ResponseStatus.Fail;
    }

    return response;
}

    public async Task<BaseResponse<Sale>> DeleteSale(int id)
    {
        var response = new BaseResponse<Sale>();

        try
        {
            response = await GetSaleById(id);

            response.Data.IsDeleted = true;

            _dbContext.Sales.Update(response.Data);

            var affectedRows = await _dbContext.SaveChangesAsync();

            if (affectedRows == 0)
            {
                response.Status = ResponseStatus.Fail;
                response.Message = "Deletion failed.";
            }
            else
            {
                response.Status = ResponseStatus.Success;
                response.Message = "Sale deleted.";
            }
        }
        catch (Exception ex)
        {
            response.Message = $"Error in SaleRepository {nameof(DeleteSale)}: {ex.Message}";
            response.Status = ResponseStatus.Fail;
        }

        return response;
    }
}
