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
            var ticket = new Ticket(Guid.NewGuid(), Ticket.TicketType.Service);

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

        [Fact]
        public void CheckOut_Apply_ShouldSetQuantityToZero_WhenAmountEqualsCurrentQuantity()
        {
            // Arrange
            var item = new Consumable(
                id: Guid.NewGuid(),
                sku: "BATT-UNIT-NWM",
                name: "Battery Unit",
                description: "Replaces battery packs",
                minQty: 10,
                location: new Location(
                    id: Guid.NewGuid(),
                    name: "B1",
                    capacity: 50),
                currentQuantity: 5);

            var checkOut = new CheckOut(
                id: Guid.NewGuid(),
                timestamp: DateTime.Now,
                item: item,
                user: new User("00002", "Blair Streetwise"),
                ticket: new Ticket(Guid.NewGuid(), Ticket.TicketType.Service),
                amount: 5);

            // Act 
            checkOut.Apply();

            // Assert
            Assert.Equal(0, item.CurrentQuantity);

            Console.WriteLine($"CheckOut applied: Expected stock = 0, Actual stock = {item.CurrentQuantity}");
        }

        [Fact]
        public void CheckOut_Apply_ShouldThrow_WhenAmountNonPositive()
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
                currentQuantity: 15);

            var user = new User("00001", "Shady George");
            var ticket = new Ticket(Guid.NewGuid(), Ticket.TicketType.Service);
            var checkOut = new CheckOut(
                Guid.NewGuid(),
                DateTime.UtcNow,
                consumable,
                user,
                ticket,
                amount: 0);

            // Act / Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => checkOut.Apply());
            Console.WriteLine("SUCCES: Expected ArgumentOutOfRangeException was thrown when CheckOut amount <= 0.");
        }

        [Fact]
        public void CheckOut_Apply_ShouldThrow_WhenAmountTooHigh()
        {
            // Arrange
            var location = new Location(Guid.NewGuid(), "B2", 20);
            var item = new Consumable(Guid.NewGuid(), "IDRAC-MOD-NWM", "iDRAC Module", "management module", 2, location, 10);
            var user = new User("00004", "Mac Black");
            var ticket = new Ticket(Guid.NewGuid(), Ticket.TicketType.Service);

            // An invalid CheckOut request - amount > available stock
            var checkout = new CheckOut(
                Guid.NewGuid(),
                DateTime.Now,
                item,
                user,
                ticket,
                15);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => checkout.Apply());
            Console.WriteLine("SUCCES: CheckOut blokkeerde een te hoge uitgifte en gooide een InvalidOperationException");
        }
    }
}   