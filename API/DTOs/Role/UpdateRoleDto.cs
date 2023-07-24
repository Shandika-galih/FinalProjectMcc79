using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Role;

public class UpdateRoleDto
{
    public Guid Guid { get; set; }
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }
}
