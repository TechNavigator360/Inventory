using System;
using Xunit;
using Inventory.Domain.Entities;

namespace Inventory.Domain.Tests.LocationTests
{
    public class Item_Location_Invariant_Tests
    {
        [Fact]
        public void Item_And_Location_ShouldRespectCapacityInvariant()
        {
            // Arrange
            var location = new Location(Guid.NewGuid(), "B2", 20);
            var consumable = new Consumable(
                id: Guid.NewGuid(),
                sku: "IDRAC-MOD-NWM",
                name: "iDRAC Module",
                description: "management module",
                minQty: 2,
                location: location,
                currentQuantity: 7);

            // Act
            var current = consumable.GetCurrentQuantity();
            var capacity = location.Capacity;

            // Assert
            Assert.InRange(current, 0, capacity);
            Assert.True(capacity > 0, "Een Location Capacity moet altijd een positief getal zijn.");

            Console.WriteLine($"SUCCES: Voorraad ({current}) ligt binnen capaciteit van locatie '{location.Name}' ({capacity}).");
        }
    }
}