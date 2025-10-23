namespace Inventory.Domain.Entities;

public class SerializedItem
{
    private Guid _id;
    private string _serialNumber;
    private bool _isAvailable;

    public Guid Id => _id;
    public string SerialNumber => _serialNumber;
    public bool IsAvailable => _isAvailable;

    protected internal SerializedItem(Guid id, string serialNumber, bool isAvailable)
    {
        _id = id;
        _serialNumber = serialNumber;
        _isAvailable = isAvailable;
    }
}