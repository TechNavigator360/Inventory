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
                sku: "CBL-SATA-50CM",
                name: "SATA Cable 50cm",
                location: new Location(
                    id: Guid.NewGuid(),
                    name: "B3",
                    capacity: 100),
                currentQuantity: 5,
                minQuantity: 10
            );

            // Act
            var result = item.NeedsReorder();

            // Assert
            Assert.True(result);
        }
    }
}
