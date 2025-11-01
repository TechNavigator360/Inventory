namespace Inventory.Domain.Entities;

public class SerializedItem
{
    private readonly Guid _id;
    private readonly string _serialNumber;
    private readonly bool _isAvailable;

    public Guid Id => _id;
    public string SerialNumber => _serialNumber;
    public bool IsAvailable => _isAvailable;

    protected internal SerializedItem(Guid id, string serialNumber, bool isAvailable)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id mag niet leeg zijn.", nameof(id));

        if (string.IsNullOrWhiteSpace(serialNumber))
            throw new ArgumentNullException(nameof(serialNumber));

        _id = id;
        _serialNumber = serialNumber;
        _isAvailable = isAvailable;
    }
    
    public void SetAvailability(bool state)
    {
        // TODO: implement domain logic in upcoming phase
        throw new NotImplementedException("SetAvailibility wordt in een volgende fase geimplementeerd.");
    }
}