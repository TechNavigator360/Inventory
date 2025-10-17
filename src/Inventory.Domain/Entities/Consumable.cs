using System.ComponentModel.Design;

namespace Inventory.Domain.Entities;

public class Consumable : Item
{
    private int _currentQuantity;

    public Consumable(Guid id, string sku, string name, string description, int minQty, Location location, int currentQuantity)
        : base(id, sku, name, description, minQty, location)
    {
        _currentQuantity = currentQuantity;
    }

    protected override void AdjustQuantity(int delta)
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