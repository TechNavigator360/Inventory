using Xunit;
using Inventory.Domain.Entities;
using System;

namespace Inventory.Domain.Tests.ItemTests
{
    public class Item_CurrentQuantity_Tests
    {
        [Fact]
        public void Consumable_ShouldThrow_WhenCurrentQuantityIsNegative()
        {
            // Arrange
            var location = new Location(Guid.NewGuid(), "Rek-A1", 100);

            //Act
            try
            {
                // nieuwe Consumable met negatieve voorraad
                var item = new Consumable(Guid.NewGuid(), "SKU-01", "Test Item", "test beschrijving", 10, location, -5);

                // Assert
                Console.WriteLine("FOUT: Er is geen uitzondering gegooid bij een negatieve voorraad (verwacht: ArgumentOutOfRangeException).");
                Assert.Fail("Er is geen uitzondering gegooid bij negatieve voorraad.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"SUCCES: Verwachte uitzondering gevangen → {ex.Message}");
                Assert.True(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FOUT: Onverwachte uitzondering gevangen → {ex.GetType().Name}: {ex.Message}");
                Assert.Fail("Onverwachte uitzondering van een ander type.");
            }
        }

        [Fact]
        public void Item_UpdateQuantity_ShouldSucceed_WhenPositiveAmount()
        {
            // Arrange
            var location = new Location(Guid.NewGuid(), "B1", 50);
            var consumable = new Consumable(
                id: Guid.NewGuid(),
                sku: "BATT-UNIT-NWM",
                name: "Battery Unit",
                description: "replaces battery packs",
                minQty: 5,
                location: location,
                currentQuantity: 10);

            var delta = 5;

            // Act
            consumable.AdjustQuantity(delta);
            var result = consumable.GetCurrentQuantity();

            // Assert
            Assert.Equal(15, result);
            Assert.True(result >= 0);

            Console.WriteLine($"SUCCES: Voorraad correct bijgewerkt, nieuwe waarde: {result}");
        }
    }
}