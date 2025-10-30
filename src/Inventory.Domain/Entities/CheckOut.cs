namespace Inventory.Domain.Entities;

public class CheckOut : Transaction
{
    private readonly int _amount;
    private readonly List<string> _serials = new();

    // Item.Consumable.CheckOut
    public CheckOut(Guid id, DateTime timestamp, Item item, User user, Ticket ticket, int amount)
        : base(id, timestamp, item, user, ticket)
    {
        _amount = amount;
        _serials = new List<string>();
    }

    // Item.SparePart.Checkout
    public CheckOut(Guid id, DateTime timestamp, Item item, User user, Ticket ticket, List<string> serials)
        : base(id, timestamp, item, user, ticket)
    {
        _serials = serials ?? new List<string>();
        _amount = _serials.Count;
    }
    protected override void Validate()
    {
        if (_amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(_amount), $"De uitgiftehoeveelheid moet groter zijn dan nul (opgegeven: {_amount}).");
            
        if (!Item.CanCheckOut(_amount))
            throw new InvalidOperationException("Uitgifte niet toegestaan: hoeveelheid ongeldig of te groot.");
    }

    protected override int GetChangeAmount()
    {
        return -_amount;
    }

    protected override void GetSerialChange()
    {
        //placeholder
    }
}