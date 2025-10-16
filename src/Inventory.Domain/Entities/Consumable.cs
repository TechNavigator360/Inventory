using System.ComponentModel.Design;

namespace Inventory.Domain.Entities;

public class Consumable : Item
{
    public Consumable(string sku, string name, Location location, int currentQuantity, int minQuantity)
        : base(currentQuantity, minQuantity)
    {
        
    }
}