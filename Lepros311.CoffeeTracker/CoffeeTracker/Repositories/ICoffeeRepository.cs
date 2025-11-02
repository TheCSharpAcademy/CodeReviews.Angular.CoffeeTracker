using CoffeeTracker.Api.Models;
using CoffeeTracker.Api.Responses;

namespace CoffeeTracker.Api.Repositories;

public interface ICoffeeRepository
{
    public Task<PagedResponse<List<Coffee>>> GetPagedCoffees(PaginationParams paginationParams);

    public Task<BaseResponse<Coffee>> GetCoffeeById(int id);

    public Task<BaseResponse<Coffee>> CreateCoffee(Coffee coffee);

    public Task<BaseResponse<Coffee>> UpdateCoffee(Coffee updatedCoffee);

    public Task<BaseResponse<Coffee>> DeleteCoffee(int id);

    //public Task<List<int>> GetAllCoffeeIds();

    //public Task<List<Coffee>> GetCoffeesByIds(IEnumerable<int> coffeeIds);
}
