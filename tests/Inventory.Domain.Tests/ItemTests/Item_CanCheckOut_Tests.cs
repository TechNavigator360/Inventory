using Xunit;
using Inventory.Domain.Entities;
using System;

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
    }
}