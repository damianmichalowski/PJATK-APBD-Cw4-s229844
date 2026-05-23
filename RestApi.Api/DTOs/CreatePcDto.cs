using System.ComponentModel.DataAnnotations;

namespace RestApi.Api.DTOs;

public class CreatePcDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public double Weight { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Warranty { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
}
