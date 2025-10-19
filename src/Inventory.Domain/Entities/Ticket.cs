namespace Inventory.Domain.Entities;

public class Ticket
{
    private Guid _id;
    public Guid Id => _id;

    public Ticket(Guid id)
    {
        _id = id;
    }

}