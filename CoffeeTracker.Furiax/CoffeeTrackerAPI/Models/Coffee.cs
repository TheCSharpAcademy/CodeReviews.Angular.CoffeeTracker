using System.ComponentModel.DataAnnotations;

namespace CoffeeTrackerAPI.Models
{
    public class Coffee
    {
        [Key]
        public int Id { get; set; }
        public string Blend { get; set; }
        public int NumberOfCups { get; set; }
        public DateTime Time { get; set; } = DateTime.UtcNow.Date;
    }
}
