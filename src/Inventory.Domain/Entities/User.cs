namespace Inventory.Domain.Entities;

public class User
{
    private string _id;
    private string _name;

    public string Id => _id;
    public string Name => _name;

    public User(string id, string name)
    {
        _id = id;
        _name = name;
    }
}