using Xunit;
using Inventory.Domain.Entities;
using System;
using System.Reflection;

namespace Inventory.Domain.Tests.ItemTests
{
    public class Item_CanCheckOut_Tests
    {
        [Fact]
        public void Item_CanCheckOut_ShouldReturnTrue_WhenAmountWithinRange()
        {
            // Arrange
            var location = new Location(Guid.NewGuid(), "Rek-A2", 100);
            var item = new Consumable(Guid.NewGuid(), "SKU-02", "Test Item", "Test beschrijving", 5, location, 10);
            int requestedAmount = 5; // Simuleert user input

            // Act
            bool result = item.CanCheckOut(requestedAmount);

            // Assert
            if (result)
            {
                Console.WriteLine("SUCCES: CanCheckOut retourneerde TRUE voor een geldige hoeveelheid binnen de voorraad.");
                Assert.True(true);
            }
            else
            {
                Console.WriteLine("FOUT: CanCheckOut retourneerde FALSE voor een geldige hoeveelheid binnen de voorraad (verwacht: TRUE).");
                Assert.Fail("CanCheckOut faalde voor een geldige hoeveelheid.");
            }
        }

        [Fact]
        public void Item_CanCheckOut_ShouldReturnFalse_WhenAmountTooHighOrZero()
        {
            // Arrange
            var location = new Location(Guid.NewGuid(), "Rek-B1", 100);
            var item = new Consumable(Guid.NewGuid(), "SKU-02", "Test Item", "beschrijving", 10, location, 10);
            var invalidAmounts = new[] { 0, 11 }; // Simuleert input
            var originalQuantity = item.GetCurrentQuantity(); // Zet waarde van CurrentQuantity om in een nieuwe variabel

            // Act
            foreach (var amount in invalidAmounts)
            {
                bool result = item.CanCheckOut(amount);

                // Assert
                if (result)
                {
                    Console.WriteLine($"FOUT: CanCheckOut retourneerde TRUE voor een ongeldige hoeveelheid ({amount}).");
                    Assert.Fail($"CanCheckOut moet FALSE retourneren voor ongeldige hoeveelheid ({amount}).");
                }
                else
                {
                    Console.WriteLine($"SUCCES (verwacht): CanCheckOut retourneerde FALSE voor ongeldige hoeveelheid ({amount}).");
                }

                Assert.Equal(originalQuantity, item.GetCurrentQuantity()); // voorraad mag niet wijzigen
            }
        }
    }
}