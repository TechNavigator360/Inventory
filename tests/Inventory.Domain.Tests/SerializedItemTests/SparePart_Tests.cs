using System;
using System.Collections.Generic;
using Xunit;
using Inventory.Domain.Entities;

namespace Inventory.Domain.Tests.SerializedItemTests
{
    public class SparePart_Tests
    {
        [Fact]
        public void SparePart_ShouldContainSerializedItems_WhenCreatedWithValidList()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var serialNumbers = new List<string> { "HHD68574521TB", "HHD68583421TB", "HHD68587871TB" };
            var location = new Location(Guid.NewGuid(), "A1", 20);

            // Act
            var sparePart = new SparePart(
                id: id,
                sku: "HDD-1TB-NWM",
                name: "1TB HDD 7200RPM",
                description: "Serial tracked 1TB HHD hot swapable",
                minQty: 2,
                location: location,
                serialNumbers: serialNumbers
            );

            // Assert
            Assert.Equal(3, sparePart.SerializedItems.Count);
            Assert.Contains(sparePart.SerializedItems, s => s.SerialNumber == "HHD68574521TB");

            Console.WriteLine("SUCCES: Verwachte 3 SerializedItems bij creatie van SparePart");
        }

        [Fact]
        public void SparePart_ShouldThrow_WhenSerializedItemListIsEmpty()
        {
            // Arrange 
            var location = new Location(
                id: Guid.NewGuid(),
                name: "A4",
                capacity: 20);

            var emptySerials = new List<string>();

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                new SparePart(
                    id: Guid.NewGuid(),
                    sku: "SP-EMPTY",
                    name: "Empty serial test.",
                    description: "Test voor UT-S1-Guard",
                    minQty: 2,
                    location: location,
                    serialNumbers: emptySerials));

            Console.WriteLine($"FOUT [UT-S1-Guard]: {ex.Message}");
        }
    }
}