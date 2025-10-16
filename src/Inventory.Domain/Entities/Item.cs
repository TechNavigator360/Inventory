namespace Inventory.Domain.Entities;

public abstract class Item
{
    public int MinQuantity { get; }
    protected int CurrentQuantity { get; set; }

    protected Item(int currentQuantity, int minQuantity)
    {
        CurrentQuantity = currentQuantity;
        MinQuantity = minQuantity;
    }

    public bool NeedsReorder()
    {
        return CurrentQuantity <= MinQuantity;
    } 

}