namespace Inventory.Domain.Entities;

public class CheckOut : Transaction
{
    private int _amount;
    private List<string> _serials = new();

    // Item.Consumable.CheckOut
    public CheckOut(Guid id, DateTime timestamp, Item item, User user, Ticket ticket, int amount)
        : base(id, timestamp, item, user, ticket)
    {
        _amount = amount;
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