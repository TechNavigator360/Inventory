using System.Reflection.Metadata;

namespace Inventory.Domain.Entities;

public abstract class Transaction
{
    private Guid _id;
    private DateTime _timestamp;
    private Item _item;
    private User _user;
    private Ticket _ticket;

    public Guid Id => _id;
    public DateTime Timestamp => _timestamp;
    public Item Item => _item;
    public User User => _user;
    public Ticket Ticket => _ticket;

    protected Transaction(Guid id, DateTime timestamp, Item item, User user, Ticket ticket)
    {
        _id = id;
        _timestamp = timestamp;
        _item = item ?? throw new ArgumentNullException(nameof(item));
        _user = user ?? throw new ArgumentNullException(nameof(user));
        _ticket = ticket ?? throw new ArgumentNullException(nameof(ticket));
    }

    public abstract void Apply();

}