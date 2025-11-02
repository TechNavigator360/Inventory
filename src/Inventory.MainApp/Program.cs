using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
using Inventory.Domain.Entities;

namespace Inventory.MainApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Inventory demo - LU14";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== INVENTORY SYSTEM FASE 1 STORY MODE ===");
            Console.ResetColor();
            Console.WriteLine();

            var narrator = new Narrator();

            // Setup
            var locationA = new Location(Guid.NewGuid(), "B1", 50);
            var locationB = new Location(Guid.NewGuid(), "A1", 20);
            var user = new User("00001", "Shady George");
            var ticket = new Ticket(Guid.NewGuid(), Ticket.TicketType.Service);

            // ===========================
            // SECTION 1: CONSUMABLE DEMO
            // ===========================
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║   SECTION 1: CONSUMABLE ITEM DEMO     ║");
            Console.WriteLine("╚═══════════════════════════════════════╝\n");
            Console.ResetColor();

            var battery = narrator.IntroduceConsumable(
                new Consumable(Guid.NewGuid(), "BATT-UNIT-NWM", "Battery Unit", "replaces battery packs", 5, locationA, 20));

            narrator.ShowReorderStatus(battery);

            var checkoutbattery = new CheckOut(Guid.NewGuid(), DateTime.Now, battery, user, ticket, 4);
            narrator.DescribeTransaction(checkoutbattery, "Consumable");
            checkoutbattery.Apply();
            narrator.ReportStock(battery);

            PauseForUser("\n[Druk op ENTER om door te gaan naar Section 2: SparePart Demo...]");

            // ===========================
            // SECTION 2: SPAREPART DEMO
            // ===========================
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n╔═══════════════════════════════════════╗");
            Console.WriteLine("║    SECTION 2: SPAREPART ITEM DEMO     ║");
            Console.WriteLine("╚═══════════════════════════════════════╝\n");
            Console.ResetColor();

            var serials = new List<string> { "HHD70226601TB", "HHD59809751TB", "HHD71126831TB", "HHD74772171TB" };
            var drive = narrator.IntroduceSparePart(
                new SparePart(Guid.NewGuid(), "HDD-1TB-NWM", "1TB HDD 7200RPM", "serial tracked, hot swapable", 2, locationB, serials));

            var checkoutDrive = new CheckOut(Guid.NewGuid(), DateTime.Now, drive, user, ticket, serials: new List<string> { "HHD70226601TB" });
            narrator.DescribeTransaction(checkoutDrive, "SparePart");
            checkoutDrive.Apply();
            narrator.ReportStock(drive);

            PauseForUser("\n[Druk op ENTER om door te gaan naar Section 3: Error Handling Demo...]");

            // ===========================
            // SECTION 3: ERROR HANDLING
            // ===========================
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n╔═══════════════════════════════════════╗");
            Console.WriteLine("║   SECTION 3: ERROR HANDLING DEMO      ║");
            Console.WriteLine("╚═══════════════════════════════════════╝\n");
            Console.ResetColor();

            narrator.ShowInvalidCheckout(battery, user, ticket);

            PauseForUser("\n[Druk op ENTER om door te gaan naar Section 4: Summary...]");

            // ===========================
            // SECTION 4: SUMMARY
            // ===========================
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔═══════════════════════════════════════╗");
            Console.WriteLine("║        SECTION 4: SUMMARY             ║");
            Console.WriteLine("╚═══════════════════════════════════════╝\n");
            Console.ResetColor();

            narrator.Summarize(battery, drive, user, ticket);

            PauseForUser("\n[Druk op ENTER om door te gaan naar OOP Principes Demonstratie...]");

            // ===========================
            //  OOP PRINCIPLES DEMONSTRATION
            // ===========================

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n╔═════════════════════════════════════╗");
            Console.WriteLine("║      OOP PRINCIPLES IN ACTION       ║");
            Console.WriteLine("╚═════════════════════════════════════╝\n");
            Console.ResetColor();

            // 1. INHERITANCE
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("🧬 1. INHERITANCE (IS-A Relationship)");
            Console.ResetColor();

            Item itemA = battery;
            Item itemB = drive;

            Console.WriteLine($"✓ {itemA.GetType().Name} IS-A Item");
            Console.WriteLine($"✓ {itemB.GetType().Name} IS-A Item");
            Console.WriteLine($"✓ Both share inherited methods: NeedsReorder(), CanCheckOut(), GetCurrentQuantity()");
            Console.WriteLine($"✓ Both have inherited properties: Id, Sku, Name, Location");
            Console.WriteLine();
            Console.WriteLine("WHY IT MATTERS:");
            Console.WriteLine("→ Code reuse: common logic written once in Item");
            Console.WriteLine("→ Consistency: all items follow same contract");
            Console.WriteLine();

            PauseForUser("[Druk op ENTER om door te gaan naar Polymorphism...]");

            // 2. POLYMORPHISM
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\n🎭 2. POLYMORPHISM (Runtime Behavior)");
            Console.ResetColor();

            Console.WriteLine("SAME METHOD, DIFFERENT BEHAVIOR:");
            Console.WriteLine($"• itemA.GetCurrentQuantity() → {itemA.GetCurrentQuantity()} (Consumable: counts _currentQuantity)");
            Console.WriteLine($"• itemB.GetCurrentQuantity() → {itemB.GetCurrentQuantity()} (SparePart: counts SerializedItems)");
            Console.WriteLine();
            Console.WriteLine($"• itemA.RequiresSerialNumber() → {itemA.RequiresSerialNumber()} (Consumable: no serials)");
            Console.WriteLine($"• itemB.RequiresSerialNumber() → {itemB.RequiresSerialNumber()} (SparePart: needs serials)");
            Console.WriteLine();
            Console.WriteLine("WHY IT MATTERS:");
            Console.WriteLine("→ Transaction.Apply() works with ANY Item type");
            Console.WriteLine("→ No if/else checking 'is this Consumable or SparePart?'");
            Console.WriteLine("→ Easy to add new item types (e.g., BatchItem) without changing existing code");
            Console.WriteLine();

            Console.WriteLine("TRANSACTION POLYMORPHISM:");
            Transaction t1 = new CheckOut(Guid.NewGuid(), DateTime.Now, battery, user, ticket, 1);
            Transaction t2 = new CheckOut(Guid.NewGuid(), DateTime.Now, drive, user, ticket, new List<string> { "HHD59809751TB" });

            int beforeBattery = battery.GetCurrentQuantity();
            int beforeDrive = drive.GetCurrentQuantity();

            t1.Apply();
            t2.Apply();

            Console.WriteLine($"• t1.Apply() → Battery: {beforeBattery} → {battery.GetCurrentQuantity()} (decreased by 1)");
            Console.WriteLine($"• t2.Apply() → Drive: {beforeDrive} → {drive.GetCurrentQuantity()} (serial logic pending Fase 2)");
            Console.WriteLine();

            PauseForUser("[Druk op ENTER om door te gaan naar Abstraction...]");

            // 3. ABSTRACTION
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n💭 3. ABSTRACTION (Hide Complexity)");
            Console.ResetColor();

            Console.WriteLine("ABSTRACT CLASSES CANNOT BE INSTANTIATED:");
            Console.WriteLine("✗ new Item(...) → COMPILE ERROR: 'Item' is abstract");
            Console.WriteLine("✗ new Transaction(...) → COMPILE ERROR: 'Transaction' is abstract");
            Console.WriteLine("✓ new Consumable(...) → OK");
            Console.WriteLine("✓ new CheckOut(...) → OK");
            Console.WriteLine();
            Console.WriteLine("ABSTRACT METHODS ENFORCE IMPLEMENTATION:");
            Console.WriteLine("• Item declares: abstract int GetCurrentQuantity()");
            Console.WriteLine("  → Consumable MUST implement: return _currentQuantity");
            Console.WriteLine("  → SparePart MUST implement: count available serials");
            Console.WriteLine();
            Console.WriteLine("WHY IT MATTERS:");
            Console.WriteLine("→ Prevents invalid objects (can't have 'generic Item')");
            Console.WriteLine("→ Forces subclasses to provide specific behavior");
            Console.WriteLine("→ Compile-time safety: forgot to override? Won't compile!");
            Console.WriteLine();

            PauseForUser("[Druk op ENTER om door te gaan naar Encapsulation...]");

            // 4. ENCAPSULATION
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n🔒 4. ENCAPSULATION (Data Hiding & Controlled Access)");
            Console.ResetColor();

            Console.WriteLine("FIELDS ARE PRIVATE READONLY:");
            Console.WriteLine($"✓ battery.Name (public property) → '{battery.Name}'");
            Console.WriteLine($"✓ battery.Id (public property) → '{battery.Id}'");
            Console.WriteLine($"✗ battery._currentQuantity → COMPILE ERROR: inaccessible due to protection level");
            Console.WriteLine($"✗ battery._id = Guid.NewGuid() → COMPILE ERROR: readonly field");
            Console.WriteLine();
            Console.WriteLine("STATE CHANGES ONLY VIA CONTROLLED METHODS:");
            int stockBefore = battery.GetCurrentQuantity();
            Console.WriteLine($"• Before: battery stock = {stockBefore}");
            Console.WriteLine("• Attempting: battery._currentQuantity = 999 → ❌ NOT POSSIBLE (private)");
            Console.WriteLine("• Valid way: checkOut.Apply() → mutates via AdjustQuantity()");

            var validCheckout = new CheckOut(Guid.NewGuid(), DateTime.Now, battery, user, ticket, 3);
            validCheckout.Apply();
            Console.WriteLine($"• After Apply(): battery stock = {battery.GetCurrentQuantity()}");
            Console.WriteLine();
            Console.WriteLine("WHY IT MATTERS:");
            Console.WriteLine("→ Prevents invalid state (negative stock impossible via constructor guards)");
            Console.WriteLine("→ Centralized validation (all changes go through Transaction.Apply())");
            Console.WriteLine("→ Audit trail (DI-5: all Transaction fields immutable after creation)");
            Console.WriteLine();

            PauseForUser("[Druk op ENTER om naar de samenvatting te gaan...]");

            // SUMMARY
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔═══════════════════════════════════════╗");
            Console.WriteLine("║   ALL 4 OOP PRINCIPLES DEMONSTRATED   ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine("\nDemo compleet! Druk op ENTER om af te sluiten...");
            Console.ReadLine();
        }

        static void PauseForUser(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.ReadLine();
        }
    }

    internal class Narrator
    {
        public Consumable IntroduceConsumable(Consumable c)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"🔋 Hi! I am a *Consumable* — {c.Name}");
            Console.WriteLine($"🗄️ Current stock: {c.GetCurrentQuantity()} units on {c.Location.Name}\n");
            Console.ResetColor();
            return c;
        }

        public SparePart IntroduceSparePart(SparePart s)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"🧰 Hello, I am a *SparePart* — {s.Name}");
            Console.WriteLine($"📉 I track {s.GetCurrentQuantity()} serialized items:");
            foreach (var item in s.SerializedItems)
                Console.WriteLine($"   • {item.SerialNumber}");
            Console.WriteLine();
            Console.ResetColor();
            return s;
        }

        public void DescribeTransaction(CheckOut checkout, string itemType)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"🛒 I am a *CheckOut* for {itemType}!");
            Console.WriteLine($" I'm checking out 4 units of {checkout.Item.Name}...");
            Console.ResetColor();
        }

        public void ShowReorderStatus(Item i)
        {
            Console.WriteLine("🔍 Checking reorder threshold...");
            Console.WriteLine($"→ Needs reorder? {i.NeedsReorder()} (MinQty = {i.MinQuantity})\n");
        }

        public void ReportStock(Item i)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ Success! '{i.Name}' now has {i.GetCurrentQuantity()} left.\n");
            Console.ResetColor();
        }

        public void ShowInvalidCheckout(Item item, User user, Ticket ticket)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("⚠️ Attempting an invalid CheckOut (Amount = 0)...");
            Console.ResetColor();

            try
            {
                var invalid = new CheckOut(Guid.NewGuid(), DateTime.Now, item, user, ticket, 0);
                invalid.Apply();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"💥 Exception caught: {ex.Message}\n");
                Console.ResetColor();
            }
        }

        public void Summarize(Consumable c, SparePart s, User u, Ticket t)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== SUMMARY ===");
            Console.ResetColor();

            Console.WriteLine($"• {c.Name}: {c.GetCurrentQuantity()} left on {c.Location.Name}");
            Console.WriteLine($"• {s.Name}: {s.GetCurrentQuantity()} serialized units remaining");
            Console.WriteLine($"• Transactions performed by: {u.Name}");
            Console.WriteLine($"• Ticket type: {t.Type}\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== END OF FUNCTIONAL DEMO ===");
            Console.ResetColor();
        }
    }
}