using System;
using Xunit;
using Inventory.Domain.Entities;

namespace Inventory.Domain.Entities.LocationTests
{
    public class Location_Valid_Capacity
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
    }
}