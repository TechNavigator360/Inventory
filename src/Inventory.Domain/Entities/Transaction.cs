namespace Inventory.Domain.Entities;

public abstract class Transaction
{
    private readonly Guid _id;
    private readonly DateTime _timestamp;
    private readonly Item _item;
    private readonly User _user;
    private readonly Ticket _ticket;

    public Guid Id => _id;
    public DateTime Timestamp => _timestamp;
    public Item Item => _item;
    public User User => _user;
    public Ticket Ticket => _ticket;

    protected Transaction(Guid id, DateTime timestamp, Item item, User user, Ticket ticket)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id mag niet leeg zijn.", nameof(id));
            
        _id = id;
        _timestamp = timestamp;
        _item = item ?? throw new ArgumentNullException(nameof(item));
        _user = user ?? throw new ArgumentNullException(nameof(user));
        _ticket = ticket ?? throw new ArgumentNullException(nameof(ticket));
    }

    public void Apply()
    {
        Validate();
        int delta = GetChangeAmount();
        GetSerialChange();
        _item.AdjustQuantity(delta);
    }

    protected abstract void Validate();
    protected abstract int GetChangeAmount();
    protected abstract void GetSerialChange();
}        