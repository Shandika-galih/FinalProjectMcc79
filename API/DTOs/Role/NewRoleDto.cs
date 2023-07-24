using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Role;

public class NewRoleDto
{
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }
}
