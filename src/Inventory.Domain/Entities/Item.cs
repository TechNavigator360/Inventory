namespace Inventory.Domain.Entities;

public abstract class Item
{
    private Guid _id;
    private string _sku;
    private string _name;
    private string _description;
    private int _minQuantity;
    private Location _location;

    public Guid Id => _id;
    public string Sku => _sku;
    public string Name => _name;
    public string Description => _description;
    public int MinQuantity => _minQuantity;
    public Location Location => _location;

    protected Item(Guid id, string sku, string name, string description, int minQty, Location location)
    {
        _id = id;
        _sku = sku ?? throw new ArgumentNullException(nameof(sku));
        _name = name ?? throw new ArgumentNullException(nameof(name));
        _description = description ?? string.Empty;
        _minQuantity = minQty;
        _location = location ?? throw new ArgumentNullException(nameof(location));
    }

    public bool NeedsReorder()
    {
        return GetCurrentQuantity() <= _minQuantity;
    }

    public bool CanCheckOut(int amount)
    {
        return amount > 0 && amount <= GetCurrentQuantity();
    }
    public abstract int GetCurrentQuantity();
    public abstract bool RequiresSerialNumber();
    protected abstract void AdjustQuantity(int delta);
}