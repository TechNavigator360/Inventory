namespace Inventory.Domain.Entities;

public class Location
{
    private Guid _id;
    private string _name;
    private int _capacity;

    public Guid Id => _id;
    public string Name => _name;
    public int Capacity => _capacity;

    public Location(Guid id, string name, int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capaciteit moet groter zijn dan nul.");

        _id = id;
        _name = name;
        _capacity = capacity;
    }
}