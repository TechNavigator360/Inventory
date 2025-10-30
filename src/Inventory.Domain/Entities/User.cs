namespace Inventory.Domain.Entities;

public class User
{
    private readonly string _id;
    private readonly string _name;

    public string Id => _id;
    public string Name => _name;

    public User(string id, string name)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentNullException(nameof(id));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        _id = id;
        _name = name;
    }
}