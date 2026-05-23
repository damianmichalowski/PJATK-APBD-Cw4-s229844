namespace RestApi.Api.Models;

public class Component
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int ComponentManufacturersId { get; set; }
    public int ComponentTypesId { get; set; }

    public ComponentManufacturer ComponentManufacturer { get; set; } = null!;
    public ComponentType ComponentType { get; set; } = null!;
    public ICollection<PCComponent> PCComponents { get; set; } = new List<PCComponent>();
}
