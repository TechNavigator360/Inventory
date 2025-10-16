namespace Inventory.Domain.Entities;

public class Location
{
    public Guid Id { get; }
    public string Name { get; }
    public int Capacity { get; }

    public Location(Guid id, string name, int capacity)
    {
        Id = id;
        Name = name;
        Capacity = capacity;
    }
}