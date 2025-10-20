namespace Inventory.Domain.Entities;

public class Consumable : Item
{
    private int _currentQuantity;
    public int CurrentQuantity => _currentQuantity;

    public Consumable(Guid id, string sku, string name, string description, int minQty, Location location, int currentQuantity)
        : base(id, sku, name, description, minQty, location)
    {
        if (currentQuantity < 0)
            throw new ArgumentOutOfRangeException(nameof(currentQuantity), "CurrentQuantity, actuele voorraad mag niet negatief zijn.");
            
        _currentQuantity = currentQuantity;
    }

    protected internal override void AdjustQuantity(int delta)
    {
        _currentQuantity += delta;
    }

    public override int GetCurrentQuantity()
    {
        return _currentQuantity;
    }

    public override bool RequiresSerialNumber()
    {
        return false;
    }
}