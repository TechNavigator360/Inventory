namespace Inventory.Domain.Entities;

public abstract class Item
{
    private readonly Guid _id;
    private readonly string _sku;
    private readonly string _name;
    private readonly string _description;
    private readonly int _minQuantity;
    private readonly Location _location;

    public Guid Id => _id;
    public string Sku => _sku;
    public string Name => _name;
    public string Description => _description;
    public int MinQuantity => _minQuantity;
    public Location Location => _location;

    protected Item(Guid id, string sku, string name, string description, int minQty, Location location)
    {
        _id = id != Guid.Empty ? id : throw new ArgumentException("Id mag niet leeg zijn.", nameof(id));
        _sku = sku ?? throw new ArgumentNullException(nameof(sku));
        _name = name ?? throw new ArgumentNullException(nameof(name));
        _description = description ?? string.Empty;
        _minQuantity = minQty;
        if (minQty < 0)
            throw new ArgumentOutOfRangeException(nameof(minQty), "MinQuantity mag niet negatief zijn.");
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
    protected internal abstract void AdjustQuantity(int delta);
}