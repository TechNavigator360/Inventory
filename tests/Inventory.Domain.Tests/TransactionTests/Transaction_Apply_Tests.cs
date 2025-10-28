using System;
using Inventory.Domain.Entities;
using Xunit;

namespace Inventory.Domain.Tests.TransactionTests
{
    public class Transaction_Apply_Tests
    {
        [Fact]
        public void Transaction_Apply_ShouldValidateBeforeAdjustQuantity()
        {
            // Arrange
            var location = new Location(Guid.NewGuid(), "Rack A1", 100);
            var item = new ConsumableSpy(Guid.NewGuid(), "CBL-SATA-50CM", "SATA Cable 50cm", "Standard data cable", 10, location, 50);

            var user = new User("00002", "Blair Streetwise");
            var ticket = new Ticket(Guid.NewGuid());
            var checkout = new InstrumentedCheckOut(Guid.NewGuid(), DateTime.Now, item, user, ticket, 5);

            // Act
            checkout.Apply();

            // Assert
            Assert.True(item.ValidateWasCalledBeforeAdjust,
                "Validate() should have been called before AdjustQuantity().");
            Console.WriteLine("SUCCES: Validate() werd uitgevoerd vóór AdjustQuantity() (Template Method volgorde bevestigd).");
        }

        // This subclass only adds instrumentation for Validate()
        internal class InstrumentedCheckOut : CheckOut
        {
            private ConsumableSpy _spy;

            public InstrumentedCheckOut(Guid id, DateTime timestamp, Item item, User user, Ticket ticket, int amount)
                : base(id, timestamp, item, user, ticket, amount)
            {
                _spy = item as ConsumableSpy ?? throw new ArgumentException("Expected ConsumableSpy");
            }

            protected override void Validate()
            {
                _spy.OnValidateCalled();
            }

            protected override int GetChangeAmount() => -5;

            protected override void GetSerialChange() { /* Not applicable */ }
        }

        // Test double for Item that tracks AdjustQuantity()
        internal class ConsumableSpy : Consumable
        {
            public bool ValidateWasCalledBeforeAdjust { get; private set; }
            private bool _validateCalled;

            public ConsumableSpy(Guid id, string sku, string name, string description, int minQty, Location location, int currentQty)
                : base(id, sku, name, description, minQty, location, currentQty) { }

            public void OnValidateCalled() => _validateCalled = true;

            protected internal override void AdjustQuantity(int delta)
            {
                ValidateWasCalledBeforeAdjust = _validateCalled;
                base.AdjustQuantity(delta);
            }
        }
    }
}
