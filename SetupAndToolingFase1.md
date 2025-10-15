# Inventory Project – Setup & Tooling Guide Fase 1

Dit document beschrijft de opzet van de ontwikkelomgeving, de gebruikte tooling, en de configuratie van versiebeheer en testframeworks.  
Het doel is om de werkwijze reproduceerbaar te maken en inzicht te geven in de technische keuzes achter het project.

---

## 1. Projectstructuur

De oplossing volgt de standaard .NET mapstructuur:

Inventory/
│
├── coveragereport
│
├── docs/
│ ├── README.md
│ └── SetupAndToolingGuideFase1.md
│
├── src/
│ ├── Inventory.Domain/
│ ├── Inventory.Application/
│ ├── Inventory.Infrastructure.InMemory/
│ └── Inventory.MainApp/
│
├── tests/
│ ├── Inventory.Domain.Tests/
│ └── Inventory.Application.Tests/
│
└── Inventory.sln


- `src` bevat alle productcode.  
- `tests` bevat alle unittests en integratietests.  
- `docs` bevat de documentatie en verantwoordingsbestanden voor LU14 en LU15.  
- `Inventory.sln` koppelt alle projecten samen tot één oplossing.

---

## 2. Git & GitHub

### Initialisatie

Initialiseer een nieuwe repository:

```
bash
git init -b main
dotnet new sln -n Inventory
```
### Koppeling met GitHub
#### Maak een remote repository
`gh repo create <RepositoryNaam> --public --source=. --remote=origin`
#### Controleer de remote
`git remote -v`
#### Voer de eerste commit en push uit
```
git add .
git commit -m "Initial commit"
git push -u origin main
```
## 3. Projecten toevoegen aan de solution
```
dotnet sln add src/Inventory.Domain/Inventory.Domain.csproj
dotnet sln add src/Inventory.Application/Inventory.Application.csproj
dotnet sln add tests/Inventory.Domain.Tests/Inventory.Domain.Tests.csproj
```
### Controleer de inhoud
`dotnet sln list`

## 4. Testframeworkconfiguratie (xUnit)
#### Maak een testproject
```
dotnet new xunit -n Inventory.Domain.Tests -o tests/Inventory.Domain.Tests
```
#### Koppel het domeinproject
```
dotnet add tests/Inventory.Domain.Tests/Inventory.Domain.Tests.csproj reference src/Inventory.Domain/Inventory.Domain.csproj
```
#### Controleer of het testproject de juiste referenties bevat
```
<ItemGroup>
  <PackageReference Include="xunit" Version="2.*" />
  <PackageReference Include="xunit.runner.visualstudio" Version="2.*" />
  <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.*" />
</ItemGroup>
```
#### Run tests
`dotnet test`

## 5. Code Coverage (Coverlet)

#### Installatie 
`dotnet add tests/Inventory.Domain.Tests package coverlet.collector`

#### Uitvoeren met coverage
`dotnet test --collect:"XPlat Code Coverage"`

#### De output bevat een pad naar een XML-bestand in Cobertura-formaat, bijvoorbeeld
`tests/Inventory.Domain.Tests/TestResults/<GUID>/coverage.cobertura.xml`

## 6. HTML Coverage Rapport (ReportGenerator)

#### Installatie
```
dotnet tool install --global dotnet-reportgenerator-globaltool
```
#### Rapport genereren
```
reportgenerator -reports:tests/**/coverage.cobertura.xml -targetdir:coveragereport
```
#### Open het rapport
`start coveragereport/index.html`

**Resultaat:**

Regeldekking (%) en branchdekking (%) per klasse/methode.
Visueel inzicht in geteste en ongeteste code.

## 7. Testconventies

| **Element** | **Conventie** | **Voorbeeld** |
|--------------|---------------|----------------|
| **Bestandsnaam** | `<TypeNaam>Tests.cs` | `ItemTests.cs` |
| **Methodenaam** | `Class_Method_Scenario_ExpectedResult` | `Item_CanCheckOut_WhenAmountTooHigh_ShouldReturnFalse()` |
| **Structuur** | AAA (Arrange – Act – Assert) | Logische scheiding tussen fasen |
| **Exceptions** | Domeinspecifiek en concreet | `"Onvoldoende voorraad"` |

## 8. Iteratieve Cyclus (TDD)

Elke implementatiecyclus volgt de vaste TDD-stappen:

| **Fase** | **Actie** | **Commit-prefix** |
|-----------|------------|-------------------|
| **Red** | Schrijf een falende test voor nieuwe functionaliteit | `test:` |
| **Green** | Schrijf minimale code om de test te laten slagen | `feat:` |
| **Refactor** | Verbeter structuur zonder gedrag te wijzigen | `refactor:` |

**Voorbeeld:**

```
bash
git add .
git commit -m "test: Item_NeedsReorder_ShouldReturnTrue_WhenQuantityBelowMin"
git commit -m "feat: Implemented NeedsReorder logic"
git commit -m "refactor: Simplified capacity validation"
```
## 9. Coverage- en kwaliteitsdoelen

| **Metric**        | **Streefwaarde**              | **Toelichting**                                 |
|--------------------|-------------------------------|--------------------------------------------------|
| **Regeldekking**  | ≥ 85 %                        | Alle relevante code wordt geraakt tijdens tests |
| **Branchdekking** | 100 % op kritieke logica      | Alle beslispaden (true/false) worden getest     |
| **Coverage-tool** | `coverlet.collector`          | Verzamelt resultaten tijdens testuitvoering     |
| **Rapport**       | `reportgenerator` HTML-output | Visuele controle op dekking                     |
## 10. Referenties

Microsoft Docs. Unit testing C# in .NET using dotnet test and xUnit. (2025)

Microsoft Docs. Use code coverage for unit testing. (2024)

Coverlet Documentation. coverlet.collector. (2025)

Fowler, M. Mocks Aren’t Stubs. (2007)