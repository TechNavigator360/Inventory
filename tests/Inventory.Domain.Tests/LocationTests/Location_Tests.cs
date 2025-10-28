using System;
using Xunit;
using Inventory.Domain.Entities;

namespace Inventory.Domain.Tests.LocationTests
{
    public class Location_Tests
    {
        [Fact]
        public void Location_ShouldThrow_WhenCapacityIsZeroOrNegative()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(
                delegate
                {
                    Location locationA1 = new Location(
                        id: Guid.NewGuid(),
                        name: "A1",
                        capacity: 0
                    );
                }
            );

            Assert.Throws<ArgumentOutOfRangeException>(
                delegate
                {
                    Location locationA2 = new Location(
                        id: Guid.NewGuid(),
                        name: "A2",
                        capacity: -5
                    );
                }
            );

            Console.WriteLine("SUCCES: Verwachte ArgumentOutOfRangeException voor capaciteit ≤ 0.");
        }

        [Fact]
        public void Location_ShouldHaveDefinedCapacity_WhenCreated()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "B2";
            var capacity = 20;

            // Act
            var location = new Location(id, name, capacity);

            // Assert
            Assert.Equal(id, location.Id);
            Assert.Equal(name, location.Name);
            Assert.Equal(capacity, location.Capacity);
            Assert.True(location.Capacity > 0, "Location capacity moet groter zijn dan null.");

            Console.WriteLine($"SUCCES: Location '{location.Name}' aangemaakt met geldige capaciteit ({location.Capacity}).");
        }
    }
}