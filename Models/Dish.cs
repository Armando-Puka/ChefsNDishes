using System.ComponentModel.DataAnnotations;
namespace ChefsNDishes.Models;
public class Dish
{
    [Key]
    public int DishId { get; set; }

    [Required(ErrorMessage = "Name of Dish is required")]
    public string DishName { get; set; }

    [Required(ErrorMessage = "Dish Calories is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Calories must be greater than 0")]
    public int DishCalories { get; set; }

    [Required(ErrorMessage = "Tastiness must be between 1 and 5")]
    [Range(1, 5, ErrorMessage = "Tastiness must be between 1 and 5")]
    public int DishTastiness { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public Chef? Chef { get; set; }
    
    public int ChefId { get; set; }
}