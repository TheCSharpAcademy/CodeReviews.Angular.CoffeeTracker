using CoffeeTracker.Api.Models;
using CoffeeTracker.Api.Repositories;
using CoffeeTracker.Api.Responses;

namespace CoffeeTracker.Api.Services;

public class CoffeeService : ICoffeeService
{
    private readonly ISaleRepository _saleRepository;

    private readonly ICoffeeRepository _coffeeRepository;

    public CoffeeService(ICoffeeRepository coffeeRepository, ISaleRepository saleRepository)
    {
        _coffeeRepository = coffeeRepository;
        _saleRepository = saleRepository;
    }

    public async Task<PagedResponse<List<CoffeeDto>>> GetPagedCoffees(PaginationParams paginationParams)
    {
        var response = await _coffeeRepository.GetPagedCoffees(paginationParams);
        
        var responseWithDataDto = new PagedResponse<List<CoffeeDto>>(data: new List<CoffeeDto>(),
                                               pageNumber: paginationParams.Page,
                                               pageSize: paginationParams.PageSize,
                                               totalRecords: response.TotalRecords);

        if (response.Status == ResponseStatus.Fail)
        {
            responseWithDataDto.Status = response.Status;
            responseWithDataDto.Message = response.Message;
            return responseWithDataDto;
        }

        responseWithDataDto.Data = response.Data.Select(c => new CoffeeDto
        {
            Id = c.Id,
            Name = c.Name,
            Price = c.Price.ToString(),
        }).ToList();

        return responseWithDataDto;
    }

    public async Task<BaseResponse<Coffee>> GetCoffeeById(int id)
    {
        return await _coffeeRepository.GetCoffeeById(id);
    }

    public async Task<BaseResponse<CoffeeDto>> CreateCoffee(CoffeeDto writeCoffeeDto)
    {
        var response = new BaseResponse<Coffee>();
        var responseWithDataDto = new BaseResponse<CoffeeDto>();

        var newCoffee = new Coffee
        {
            Name = writeCoffeeDto.Name,
            Price = decimal.Parse(writeCoffeeDto.Price)
        };

        response = await _coffeeRepository.CreateCoffee(newCoffee);

        if (response.Status == ResponseStatus.Fail)
        {
            responseWithDataDto.Status = ResponseStatus.Fail;
            responseWithDataDto.Message = response.Message;
            return responseWithDataDto;
        }
        else
        {
            responseWithDataDto.Status = ResponseStatus.Success;

            var newCoffeeDto = new CoffeeDto
            {
                Id = newCoffee.Id,
                Name = newCoffee.Name,
                Price = newCoffee.Price.ToString()
            };

            responseWithDataDto.Data = newCoffeeDto;
        }

        return responseWithDataDto;
    }

    public async Task<BaseResponse<Coffee>> UpdateCoffee(int id, CoffeeDto CoffeeDto)
    {
        var response = new BaseResponse<Coffee>();

        response = await GetCoffeeById(id);

        if (response.Status == ResponseStatus.Fail)
        {
            return response;
        }

        var existingCoffee = response.Data;

        existingCoffee.Name = CoffeeDto.Name;
        existingCoffee.Price = decimal.Parse(CoffeeDto.Price);

        response = await _coffeeRepository.UpdateCoffee(existingCoffee);

        return response;
    }

    public async Task<BaseResponse<Coffee>> DeleteCoffee(int id)
    {
        var response = new BaseResponse<Coffee>();

        response = await _coffeeRepository.GetCoffeeById(id);

        if (response.Status == ResponseStatus.Fail)
        {
            return response;
        }

        return await _coffeeRepository.DeleteCoffee(id);
    }
}
