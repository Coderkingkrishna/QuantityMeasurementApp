# QuantityMeasurementApp

## 📌 Project Overview

QuantityMeasurementApp is a domain-driven C# application developed incrementally using a hybrid approach of:

- Design-Develop-Test (DDT)
- Test-Driven Development (TDD)

Each use case expands functionality in a controlled and maintainable way.

---


## 🚀 Use Case 2 (UC2) – Feet and Inches Measurement Equality

### Description

UC2 extends UC1 by introducing equality comparison for Inches measurement alongside Feet measurement.

⚠ Important:
- Feet and Inches are treated as separate entities.
- No cross-unit comparison (e.g., 1 ft ≠ 12 inches in this use case).
- Only same-unit equality comparison is supported.

---

## ✅ Features Implemented in UC2

- Immutable `Feet` value object
- Immutable `Inches` value object
- Proper override of `Equals()` and `GetHashCode()`
- Implementation of `IEquatable<T>` for both classes
- Equality methods in `QuantityMeasurementService`
- MSTest unit test coverage for both Feet and Inches
- Clean architecture separation (src and tests layers)

---

## 🚀 Use Case 3 (UC3) – QuantityLength with Unit Conversion (DRY)

### Description

UC3 refactors the duplicated Feet and Inches logic into a single QuantityLength class that uses a LengthUnit enum.
This solves the DRY problem where Feet and Inches had nearly identical constructors and equality logic.

### Problem Solved

- Eliminates code duplication between Feet and Inches classes.
- Enables cross-unit comparison (e.g., 1 ft == 12 inches) using a shared conversion path.
- Makes it easier to add new length units without creating new classes.

---

## ✅ Features Implemented in UC3

- LengthUnit enum with conversion factors
- QuantityLength value object with unit-aware equality
- Cross-unit comparison via base-unit conversion (feet)
- Updated QuantityMeasurementService to support QuantityLength
- New unit tests for cross-unit and same-unit equality

---

## 🧠 Concepts Demonstrated in UC3

- DRY Principle (single class for multiple units)
- Abstraction of conversion logic
- Polymorphism via unit enum
- Value-based equality with cross-unit support
- Scalability for adding new units

---

## 🚀 Use Case 4 (UC4) – Extended Unit Support (Yards & Centimeters)

### Description

UC4 extends UC3 by introducing Yards and Centimeters as additional length units to the QuantityLength class.
This demonstrates how the generic QuantityLength design scales effortlessly to accommodate new units without code duplication.

### Problem Solved

- Validates the DRY principle design from UC3 by adding new units with minimal code changes.
- Proves that adding units requires only enum modification, not new classes.
- Enables complex multi-unit conversions (yards ↔ feet ↔ inches ↔ centimeters).

---

## ✅ Features Implemented in UC4

- LengthUnit enum extended with Yards and Centimeters constants
- Conversion factors: 1 Yard = 3 Feet, 1 cm = 1/30.48 Feet
- QuantityLength unchanged – no modifications needed for new units
- 32 new test cases covering yard, centimeter, and multi-unit comparisons
- All UC1, UC2, UC3 functionality remains backward compatible

---

## 🧠 Concepts Demonstrated in UC4

- **Scalability of Generic Design**: Adding units requires only enum modification
- **Conversion Factor Management**: Centralized in enum for consistency
- **Unit Relationships**: Understanding conversion to common base unit (feet)
- **Enum Extensibility**: Type-safe approach to managing unit variations
- **Mathematical Accuracy**: Precise conversion factors relative to base unit
- **DRY Validation**: Proves the QuantityLength design eliminates duplication
- **Backward Compatibility**: New units don't affect existing functionality

---

## 🚀 Use Case 5 (UC5) – Unit-to-Unit Conversion (Same Measurement Type)

### Description

UC5 extends UC4 by adding explicit unit conversion APIs for length values.
Instead of only checking equality, the app now converts values between supported units (Feet, Inches, Yards, Centimeters) through a shared base-unit normalization path.

### Problem Solved

- Provides direct conversion for same measurement type (length) using one consistent formula.
- Keeps conversion logic centralized and reusable.
- Preserves immutability by returning converted values/new instances without mutating existing objects.

---

## ✅ Features Implemented in UC5

- Non-static conversion API in `QuantityLength`: `ConvertTo(LengthUnit targetUnit)`
- Non-static conversion API in `QuantityMeasurementService`: `Convert(double value, LengthUnit sourceUnit, LengthUnit targetUnit)`
- Conversion formula: `result = value × (sourceFactor / targetFactor)`
- Input validation for finite values and supported enum units
- Separate focused conversion tests in `UnitConversionTests.cs`

---

## 🧠 Concepts Demonstrated in UC5

- Base-unit normalization for cross-unit conversion
- Enum-driven conversion factor management
- Value object immutability and conversion without side effects
- Precision handling with epsilon-based assertions in tests
- Clear API design for conversion and equality responsibilities

---

## 🧠 Concepts Demonstrated

- Equality Contract:
  - Reflexive
  - Symmetric
  - Transitive
  - Consistent
  - Null Handling
- Type Safety
- Value-Based Equality
- Floating-point comparison using `double.Compare()`
- Encapsulation and Immutability
- SOLID Principles
- Clean project layering
- Professional Git branching workflow

---

## 🛠 Tech Stack

- .NET 8
- C#
- MSTest
- Git

---

## 📂 Project Structure

```text
QuantityMeasurementApp.sln

src/
└── QuantityMeasurementApp
    ├── Program.cs
    ├── Models/
    │   ├── Feet.cs
    │   ├── Inches.cs
    │   ├── LengthUnit.cs
    │   └── Quantity.cs
    └── Services/
        └── QuantityMeasurementService.cs

tests/
└── QuantityMeasurementApp.Tests
    ├── FeetTests.cs
    ├── InchesTests.cs
   ├── QuantityTests.cs
   └── UnitConversionTests.cs
```
## ▶ How to Run the Application

1. Clone the repository:
   git clone <repository-url>

2. Navigate to the project folder:
   cd QuantityMeasurementApp

3. Build the project:
   dotnet build

4. Run the application:
   cd src/QuantityMeasurementApp
   dotnet run

Expected Output:

Input: 1.0 inch and 1.0 inch  
Output: Equal (True)

Input: 1.0 ft and 1.0 ft  
Output: Equal (True)

UC3 Example:

Input: Quantity(1.0, Feet) and Quantity(12.0, Inches)
Output: Equal (True)

UC4 Examples:

Input: Quantity(1.0, Yards) and Quantity(3.0, Feet)
Output: Equal (True)

Input: Quantity(1.0, Yards) and Quantity(36.0, Inches)
Output: Equal (True)

Input: Quantity(2.0, Centimeters) and Quantity(2.0, Centimeters)
Output: Equal (True)

Input: Quantity(1.0, Centimeters) and Quantity(0.393701, Inches)
Output: Equal (True)

UC5 Examples:

Input: convert(1.0, Feet, Inches)
Output: 12.0

Input: convert(3.0, Yards, Feet)
Output: 9.0

Input: convert(36.0, Inches, Yards)
Output: 1.0

Input: convert(1.0, Centimeters, Inches)
Output: 0.393700787...

Input: convert(0.0, Feet, Inches)
Output: 0.0

---

## 🧪 How to Run Unit Tests

From solution root directory:

dotnet test

All test cases must pass before merging into develop or main branch.

---