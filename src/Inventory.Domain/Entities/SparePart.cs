namespace Inventory.Domain.Entities;

public class SparePart : Item
{
    private HashSet<SerializedItem> _serialItems;
    public IReadOnlyCollection<SerializedItem> SerializedItems => _serialItems;

    public SparePart(
        Guid id,
        string sku,
        string name,
        string description,
        int minQty,
        Location location,
        List<string> serialNumbers)
        : base(id, sku, name, description, minQty, location)
    {
        _serialItems = new HashSet<SerializedItem>();
        foreach (string sn in serialNumbers)
        {
            _serialItems.Add(new SerializedItem(Guid.NewGuid(), sn, true));
        }
    }
    public bool HasSerialNumber(string sn)
    {
        return _serialItems.Any(s => s.SerialNumber == sn);
    }

    public override int GetCurrentQuantity()
    {
        return _serialItems.Count;
    }

    public override bool RequiresSerialNumber()
    {
        return true;
    }

    protected internal override void AdjustQuantity(int delta)
    {
        
    }
}