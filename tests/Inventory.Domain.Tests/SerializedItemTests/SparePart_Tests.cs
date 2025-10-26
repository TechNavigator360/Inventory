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

        [Fact]
        public void SparePart_ShouldThrow_WhenDuplicateSerialsProvided()
        {
            // (duplicate DI-1)
            // Arrange
            var location = new Location(Guid.NewGuid(), "A2", 20);

            var serialsWithDuplicate = new List<string> { "HHD20145872TB", "HHD20164562TB", "HHD20183252TB", "HHD20183252TB" };

            // Act & Assert 
            var ex = Assert.Throws<ArgumentException>(() =>
            new SparePart(
                id: Guid.NewGuid(),
                sku: "HDD-2TB-NWM",
                name: "2TB HDD 7200RPM",
                description: "Duplicate serial HHD20183252TB test.",
                minQty: 2,
                location: location,
                serialNumbers: serialsWithDuplicate));

            Console.WriteLine($"FOUT: [UT-S1-Structure | DI-1]: {ex.Message}");
        }

        [Fact(Skip = "Wordt uitgevoerd in Fase 2 wanneer IsAvailable muteerbaar is.")]
        public void SparePart_GetCurrentQuantity_ShouldCountOnlyAvailableItems()
        {
      
            // (availibility DI-2)
            // Arrange
            var location1 = new Location(Guid.NewGuid(), "A3", 20);

            var serials = new List<string> { "HHD94693114TB", "HHD17157004TB", "HHD27504584TB", "HHD05683134TB" };
            var sparePart = new SparePart(
                id: Guid.NewGuid(),
                sku: "HDD-4TB-NWM",
                name: "4TB HDD 7200RPM",
                description: "Is HHD94693114TB available?",
                minQty: 2,
                location: location1,
                serialNumbers: serials);

            // Simulate HHD94693114TB unavailable
            var firstItem = sparePart.SerializedItems.First();
            typeof(SerializedItem)
                .GetProperty("IsAvailable")!
                .SetValue(firstItem, false);

            // Act
            var currentQty = sparePart.GetCurrentQuantity();

            // Assert
            Assert.NotEqual(sparePart.SerializedItems.Count, currentQty);
            Console.WriteLine($"FOUT: [UT-S1-Structure | DI-2]: GetCurrentQuantity() telt nog onbeschikbare item HHD94693114TB mee.");
        }
    }
}