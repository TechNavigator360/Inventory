namespace Inventory.Domain.Entities;
public class Ticket
{
    private readonly Guid _id;
    private readonly TicketType _type;
    private readonly string? _externalKey;

    public Guid Id => _id;
    public TicketType Type => _type;
    public string? ExternalKey => _externalKey;

    public Ticket(Guid id, TicketType type, string? externalKey = null)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id mag niet leeg zijn.", nameof(id));

        _id = id;
        _type = type;
        _externalKey = externalKey;
    }

    // temporary placeholder for compilation until full enum is implemented
    public enum TicketType
    {
        Service,
        Reorder,
        AuditCorrection
    }
}