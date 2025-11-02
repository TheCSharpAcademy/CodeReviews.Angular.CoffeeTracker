using CoffeeTracker.Api.Models;
using CoffeeTracker.Api.Responses;

namespace CoffeeTracker.Api.Services;

public interface ICoffeeService
{
    Task<PagedResponse<List<CoffeeDto>>> GetPagedCoffees(PaginationParams paginationParams);

    Task<BaseResponse<Coffee>> GetCoffeeById(int id);

    Task<BaseResponse<CoffeeDto>> CreateCoffee(CoffeeDto coffee);

    Task<BaseResponse<Coffee>> UpdateCoffee(int id, CoffeeDto coffee);

    Task<BaseResponse<Coffee>> DeleteCoffee(int id);
}
