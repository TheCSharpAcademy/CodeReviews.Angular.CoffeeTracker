namespace CoffeeTracker.Api.Models;

public class PaginationParams
{
    private const int MaxPageSize = 50;
    public int Page { get; set; } = 1;

    private int _pageSize = 10;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    public string? SortBy { get; set; }

    public bool? SortAscending { get; set; }

    public string? Name { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public int? Id { get; set; }

    public int? CoffeeId { get; set; }

    public DateOnly? MinDateOfSale { get; set; }

    public DateOnly? MaxDateOfSale { get; set; }
}
