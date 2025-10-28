using Xunit;
using Inventory.Domain.Entities;
using System.ComponentModel;

namespace Inventory.Domain.Tests.ItemTests
{
    public class Item_NeedsReorder_Tests
    {
        [Fact]
        public void Item_NeedsReorder_ShouldBeTrue_WhenBelowMinimum()
        {
            // Arrange
            var item = new Consumable(
                id: Guid.NewGuid(),
                sku: "CBL-SATA-50CM",
                name: "SATA Cable 50cm",
                description: "Standard SATA cable 50cm",
                minQty: 10,
                location: new Location(
                    id: Guid.NewGuid(),
                    name: "B3",
                    capacity: 100),
                currentQuantity: 5
            );

            // Act
            var result = item.NeedsReorder();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Item_NeedsReorder_ShouldReturnFalse_WhenAboveMinimum()
        {
            // Arrange
            var location = new Location(Guid.NewGuid(), "B3", 100);
            var consumable = new Consumable(
                id: Guid.NewGuid(),
                sku: "CBL-SATA-50CM",
                name: "SATA Cable 50cm",
                description: "standard SATA cable",
                minQty: 10,
                location: location,
                currentQuantity: 20);

            // Act
            var needsReorder = consumable.NeedsReorder();

            // Assert
            Assert.False(needsReorder);
            Console.WriteLine("SUCCES: NeedsReorder() retourneert false bij voorraad boven minimum.");
        }
    }
}
