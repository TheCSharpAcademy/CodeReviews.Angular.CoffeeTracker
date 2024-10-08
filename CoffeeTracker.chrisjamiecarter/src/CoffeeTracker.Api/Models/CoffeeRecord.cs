using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeTracker.Api.Models;

/// <summary>
/// Represents a CoffeeRecord model. This acts as both the domain and infrastructure version.
/// </summary>
[Table("CoffeeRecord")]
public class CoffeeRecord
{
    #region Properties

    [Key]
    public required Guid Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [DataType(DataType.Date), Required]
    public required DateTime Date { get; set; }

    #endregion
}
