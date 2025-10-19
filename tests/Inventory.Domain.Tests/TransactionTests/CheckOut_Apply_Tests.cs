using System;
using Inventory.Domain.Entities;
using Xunit;

namespace Inventory.Domain.Tests.TransactionTests
{
    public class CheckOut_Apply_Tests
    {
        [Fact]
        public void CheckOut_Apply_ShouldDecreaseQuantity_WhenValid()
        {
            // Arrange
            var consumable = new Consumable(
                id: Guid.NewGuid(),
                sku: "CBL-SATA-50CM",
                name: "SATA Cable 50cm",
                description: "Standard SATA cable 50cm",
                minQty: 10,
                location: new Location(
                    id: Guid.NewGuid(),
                    name: "B3",
                    capacity: 100),
                currentQuantity: 50
            );

            var user = new User("00002", "Blair Streetwise");
            var ticket = new Ticket(Guid.NewGuid());

            var checkOut = new CheckOut(
                id: Guid.NewGuid(),
                timestamp: DateTime.Now,
                item: consumable,
                user: user,
                ticket: ticket,
                amount: 5
            );

            // Act
            checkOut.Apply();

            // Assert
            Assert.Equal(45, consumable.CurrentQuantity);

            Console.WriteLine("SUCCES: CheckOut verlaagt de voorraad correct met Amount.");
        }
    }
}