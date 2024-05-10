using System.ComponentModel.DataAnnotations;

namespace NoSmoking.BBualdo.API.Models;

public class SmokeLog
{
  [Key]
  public int Id { get; set; }

  [Required]
  public DateTime Date { get; set; }

  [Required]
  public int Quantity { get; set; }
}
