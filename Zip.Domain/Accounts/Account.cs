using System.ComponentModel.DataAnnotations;

namespace Zip.Domain.Entities;

public class Account
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public long Number { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
    public string Name { get; set; }

    [Required]
    public Guid UserId { get; set; }
}
