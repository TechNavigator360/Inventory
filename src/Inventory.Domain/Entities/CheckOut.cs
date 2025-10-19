namespace Inventory.Domain.Entities;

public class CheckOut : Transaction
{
    private int _amount;
    public int Amount => _amount;

    public CheckOut(Guid id, DateTime timestamp, Item item, User user, Ticket ticket, int amount)
        : base(id, timestamp, item, user, ticket)
    {
        _amount = amount;
    }

    public override void Apply()
    {
        
    }
}