using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EnergyDrinkTracker.Forser_API.Models
{
    public class Records
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DrinkDate { get; set; } = DateTime.UtcNow;
        [DisplayName("Energy Drink Name")]
        public string EnergyDrink { get; set; }
        [DisplayName("Size of Can in milliliters")]
        public int CanSize { get; set; }
    }
}