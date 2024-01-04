using System.ComponentModel.DataAnnotations;
namespace ChefsNDishes.Models;

public class Chef
{
    [Key]
    public int ChefId { get; set; }

    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Birthday is required")]
    [DataType(DataType.Date)]
    [Display(Name = "Birthday")]
    [PastDate(ErrorMessage = "Birthday must be in the past")]
    [MinimumAge(18, ErrorMessage = "Chef must be at least 18 years old")]
    public DateTime? Birthday { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<Dish> AllDishes { get; set; } = new List<Dish>();
    
    public int Age {
        get
        {
            if (Birthday.HasValue)
            {
                DateTime now = DateTime.Now;
                int age = now.Year - Birthday.Value.Year;

                if (now < Birthday.Value.AddYears(age))
                {
                    age--;
                }

                return age;
            }
            return 0;
        }
    }
}

public class PastDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            DateTime dateValue;
            if (DateTime.TryParse(value.ToString(), out dateValue))
            {
                if (dateValue.Date >= DateTime.Now.Date)
                {
                    return new ValidationResult(ErrorMessage ?? "Birthday must be in the past");
                }
            }
            else
            {
                return new ValidationResult("Invalid date format");
            }
        }

        return ValidationResult.Success;
    }
}

public class MinimumAgeAttribute : ValidationAttribute
{
    private readonly int _minimumAge;

    public MinimumAgeAttribute(int minimumAge)
    {
        _minimumAge = minimumAge;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            DateTime dateOfBirth;
            if (DateTime.TryParse(value.ToString(), out dateOfBirth))
            {
                int age = DateTime.Now.Year - dateOfBirth.Year;

                if (age < _minimumAge)
                {
                    return new ValidationResult(ErrorMessage ?? $"Chef must be at least {_minimumAge} years old");
                }
            }
            else
            {
                return new ValidationResult("Invalid date format");
            }
        }

        return ValidationResult.Success;
    }
}