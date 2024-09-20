using System.ComponentModel.DataAnnotations;

namespace Zip.Domain.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
    public string Name { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "Email cannot be longer than 50 characters.")]
    public string Email { get; set; }

    [Required]
    [Range(0, 100000000)]
    public decimal Salary { get; set; }

    [Required]
    [Range(0, 100000000)]
    public decimal Expenses { get; set; }
}
