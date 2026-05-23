namespace RestApi.Api.Models;

public class ComponentType
{
    public int Id { get; set; }
    public required string Abbreviation { get; set; }
    public required string Name { get; set; }

    public ICollection<Component> Components { get; set; } = new List<Component>();
}
