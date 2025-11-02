using CoffeeTracker.Api.Models;
using CoffeeTracker.Api.Repositories;
using CoffeeTracker.Api.Responses;
using Humanizer;
using System.Globalization;

namespace CoffeeTracker.Api.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;

    private readonly ICoffeeRepository _coffeeRepository;

    public SaleService(ICoffeeRepository coffeeRepository, ISaleRepository saleRepository)
    {
        _coffeeRepository = coffeeRepository;
        _saleRepository = saleRepository;
    }

    public async Task<PagedResponse<List<SaleDto>>> GetPagedSales(PaginationParams paginationParams)
    {
        var response = await _saleRepository.GetPagedSales(paginationParams);

        var responseWithDataDto = new PagedResponse<List<SaleDto>>(data: new List<SaleDto>(),
                                               pageNumber: paginationParams.Page,
                                               pageSize: paginationParams.PageSize,
                                               totalRecords: response.TotalRecords);

        if (response.Status == ResponseStatus.Fail)
        {
            responseWithDataDto.Status = response.Status;
            responseWithDataDto.Message = response.Message;
            return responseWithDataDto;
        }

        responseWithDataDto.Data = response.Data.Select(s => new SaleDto
        {
            Id = s.Id,
            DateAndTimeOfSale = s.DateAndTimeOfSale.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            CoffeeName = s.CoffeeName,
            CoffeeId = s.CoffeeId,
            Total = s.Total.ToString()
        }).ToList();

        return responseWithDataDto;
    }

    public async Task<BaseResponse<Sale>> GetSaleById(int id)
    {
        return await _saleRepository.GetSaleById(id);
    }

    public async Task<BaseResponse<SaleDto>> CreateSale(CreateSaleDto writeSaleDto)
    {
        var saleResponse = new BaseResponse<Sale>();
        var saleResponseWithDataDto = new BaseResponse<SaleDto>();

        var coffeeResponse = await _coffeeRepository.GetCoffeeById(writeSaleDto.CoffeeId);

        if (coffeeResponse.Status == ResponseStatus.Fail)
        {
            saleResponseWithDataDto.Status = ResponseStatus.Fail;
            saleResponseWithDataDto.Message = coffeeResponse.Message;
            return saleResponseWithDataDto;
        }

        var dateAndTimeOfSale = writeSaleDto.DateAndTimeOfSale ?? DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        
        var newSale = new Sale
        {
            CoffeeId = coffeeResponse.Data.Id,
            DateAndTimeOfSale = DateTime.Parse(dateAndTimeOfSale).ToUniversalTime(),
            Total = coffeeResponse.Data.Price
        };

        if (newSale.Total < 0)
        {
            saleResponseWithDataDto.Status = ResponseStatus.Fail;
            saleResponseWithDataDto.Message = "Sale total must be greater than 0.";
            return saleResponseWithDataDto;
        }

        newSale.Coffee = coffeeResponse.Data;
        newSale.CoffeeName = coffeeResponse.Data.Name;

        saleResponse = await _saleRepository.CreateSale(newSale);

        if (saleResponse.Status == ResponseStatus.Fail)
        {
            saleResponseWithDataDto.Status = ResponseStatus.Fail;
            saleResponseWithDataDto.Message = saleResponse.Message;
            return saleResponseWithDataDto;
        }
        else
        {
            saleResponseWithDataDto.Status = ResponseStatus.Success;

            var newSaleDto = new SaleDto
            {
                Id = newSale.Id,
                DateAndTimeOfSale = newSale.DateAndTimeOfSale.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                CoffeeName = newSale.Coffee.Name,
                CoffeeId = newSale.Coffee.Id,
                Total = newSale.Total.ToString()
            };

            saleResponseWithDataDto.Data = newSaleDto;
        }

        return saleResponseWithDataDto;
    }

    public async Task<BaseResponse<SaleDto>> UpdateSale(int id, UpdateSaleDto updateSaleDto)
{
    var saleResponseDto = new BaseResponse<SaleDto>();
    var saleResponse = await GetSaleById(id);

    if (saleResponse.Status == ResponseStatus.Fail)
    {
        saleResponseDto.Status = saleResponse.Status;
        saleResponseDto.Message = saleResponse.Message;
        return saleResponseDto;
    }

    var existingSale = saleResponse.Data;

    // Update date if provided
    if (updateSaleDto.DateAndTimeOfSale != null)
    {
        existingSale.DateAndTimeOfSale = DateTime.Parse(updateSaleDto.DateAndTimeOfSale).ToUniversalTime();
    }

    // Update coffee if provided
    if (updateSaleDto.CoffeeId != null)
    {
        var coffeeResponse = await _coffeeRepository.GetCoffeeById(updateSaleDto.CoffeeId.Value);

        if (coffeeResponse.Status == ResponseStatus.Fail)
        {
            saleResponseDto.Status = ResponseStatus.Fail;
            saleResponseDto.Message = coffeeResponse.Message;
            return saleResponseDto;
        }

        existingSale.CoffeeId = coffeeResponse.Data.Id;
        existingSale.CoffeeName = coffeeResponse.Data.Name;
        existingSale.Total = coffeeResponse.Data.Price;
    }

    // Update using the tracked entity
    var updateResponse = await _saleRepository.UpdateSale(existingSale);

    if (updateResponse.Status == ResponseStatus.Fail)
    {
        saleResponseDto.Status = updateResponse.Status;
        saleResponseDto.Message = updateResponse.Message;
        return saleResponseDto;
    }

    // Convert to DTO for response
    saleResponseDto.Status = ResponseStatus.Success;
    saleResponseDto.Data = new SaleDto
    {
        Id = updateResponse.Data.Id,
        DateAndTimeOfSale = updateResponse.Data.DateAndTimeOfSale.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
        CoffeeName = updateResponse.Data.CoffeeName,
        CoffeeId = updateResponse.Data.CoffeeId,
        Total = updateResponse.Data.Total.ToString()
    };

    return saleResponseDto;
}

    public async Task<BaseResponse<Sale>> DeleteSale(int id)
    {
        var response = new BaseResponse<Sale>();

        response = await _saleRepository.GetSaleById(id);

        if (response.Status == ResponseStatus.Fail)
        {
            return response;
        }

        return await _saleRepository.DeleteSale(id);
    }
}
