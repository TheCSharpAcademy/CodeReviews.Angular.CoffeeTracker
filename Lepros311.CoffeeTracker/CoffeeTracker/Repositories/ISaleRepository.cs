using CoffeeTracker.Api.Models;
using CoffeeTracker.Api.Responses;

namespace CoffeeTracker.Api.Repositories;

public interface ISaleRepository
{
    public Task<PagedResponse<List<Sale>>> GetPagedSales(PaginationParams paginationParams);

    public Task<BaseResponse<Sale>> GetSaleById(int id);

    public Task<BaseResponse<Sale>> CreateSale(Sale sale);

    public Task<BaseResponse<Sale>> UpdateSale(Sale updatedSale);

    public Task<BaseResponse<Sale>> DeleteSale(int id);
}
