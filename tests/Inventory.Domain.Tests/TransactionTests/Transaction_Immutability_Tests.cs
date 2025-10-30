using System;
using Inventory.Domain.Entities;
using Xunit;

namespace Inventory.Domain.Tests.TransactionTests
{
    public class Transaction_Immutability_Tests
    {
        [Fact]
        public void Transaction_ShouldRemainImmutable_AfterCreation()
        {
            // Arrange
            var location = new Location(Guid.NewGuid(), "B3", 100);
            var item = new Consumable(
                Guid.NewGuid(),
                "CBL-SATA-50CM",
                "SATA Cable 50cm",
                "standard SATA cable",
                minQty: 10,
                location: location,
                currentQuantity: 20);

            var user = new User("00005", "Mauve Spring");
            var ticket = new Ticket(Guid.NewGuid());

            // Create a valid CheckOut transaction
            var checkout = new CheckOut(
                Guid.NewGuid(),
                DateTime.Now,
                item,
                user,
                ticket,
                amount: 3);

            // Act – capture values before any operation
            var idBefore = checkout.Id;
            var timestampBefore = checkout.Timestamp;
            var itemBefore = checkout.Item;
            var userBefore = checkout.User;
            var ticketBefore = checkout.Ticket;

            // Assert – all fields remain unchanged
            Assert.Equal(idBefore, checkout.Id);
            Assert.Equal(timestampBefore, checkout.Timestamp);
            Assert.Equal(itemBefore, checkout.Item);
            Assert.Equal(userBefore, checkout.User);
            Assert.Equal(ticketBefore, checkout.Ticket);

            Console.WriteLine("SUCCES: Transaction-velden (Id, Timestamp, Item, User, Ticket) zijn onveranderlijk na creatie (DI-5).");
        }
    }
}