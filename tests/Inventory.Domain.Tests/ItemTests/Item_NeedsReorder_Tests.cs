using Xunit;
using Inventory.Domain.Entities;

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
    }
}
