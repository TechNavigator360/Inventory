# Inventory System - Demo Applicatie

**Auteur:** Rute Ferreira Rodrigues | **Module:** OOP (LU13/LU14/LU15)

---

## 🚀 Demo Uitvoeren

```bash
cd Inventory.MainApp
dotnet run
```

---

## 📋 Wat Doet De Demo?

### Deel 1: Story Mode
- Toont werking van **Consumable** items (Battery Unit)
- Toont werking van **SparePart** items (1TB HDD met serienummers)
- Voert CheckOut transacties uit
- Demonstreert validatie en error handling

### Deel 2: OOP Principes
- 🧬 **Overerving** - Consumable en SparePart erven van Item
- 🎭 **Polymorfisme** - Zelfde methoden, verschillend gedrag
- 💭 **Abstractie** - Abstracte klassen kunnen niet worden geïnstantieerd
- 🔒 **Encapsulatie** - Private velden, gecontroleerde toegang

---

## 🗂️ Project Structuur

```
Inventory/
├── Inventory.Domain/           # Domein entities
├── Inventory.MainApp/          # 👈 Deze demo (Program.cs)
└── Inventory.Domain.Tests/     # Unit tests (47 tests)
```

---

## 📊 Fase 1 Status

**Geïmplementeerd:**
- ✅ Item hiërarchie (Consumable, SparePart)
- ✅ Transactie hiërarchie (CheckOut)
- ✅ CheckOut transacties voor Consumable
- ✅ Validatie en alle 4 OOP principes

**Fase 2 (gepland):**
- CheckOut voor SparePart met seriële logica
- CheckIn, Correction, Audit

---

## 🧪 Tests Uitvoeren

```bash
cd Inventory.Domain.Tests
dotnet test
```

---

## ⚙️ Vereisten

- .NET 6.0+
- Visual Studio 2022 / VS Code / Rider
