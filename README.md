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

## 🚀 Use Case 6 (UC6) – Addition of Two Length Units (Same Category)

### Description

UC6 extends UC5 by adding arithmetic support for length values.
Two measurements can be added even when units differ, and by default the result is returned in the unit of the first operand.

### Problem Solved

- Supports same-unit and cross-unit addition through the existing conversion pipeline.
- Preserves immutability by returning new `QuantityLength` objects.
- Enables clear API usage for domain arithmetic on value objects.

---

## ✅ Features Implemented in UC6

- `QuantityLength.Add(QuantityLength other)`
- `QuantityLength.Add(QuantityLength other, LengthUnit targetUnit)`
- `QuantityLength.Add(double value, LengthUnit unit)`
- `QuantityLength.Add(double value, LengthUnit unit, LengthUnit targetUnit)`
- Service overloads for object and raw-value addition in `QuantityMeasurementService`
- Validation for null operands and invalid/unsupported units
- Dedicated UC6 test suite in `UnitAdditionTests.cs`

---

## 🧠 Concepts Demonstrated in UC6

- Same-unit and cross-unit addition
- Commutativity checks in a shared target unit
- Identity behavior with zero
- Floating-point precision handling with epsilon tolerance
- Input validation and defensive error handling

---

## 🚀 Use Case 7 (UC7) – Addition with Target Unit Specification

### Description

UC7 extends UC6 by allowing the caller to explicitly provide a target unit for the addition result.
The operation still adds values through a common base-unit path, but the final output unit is always the requested target unit.

### Problem Solved

- Decouples result representation from operand units.
- Supports flexible output in FEET, INCHES, YARDS, or CENTIMETERS for the same arithmetic operation.
- Preserves immutability by returning a new `QuantityLength` instance.

---

## ✅ Features Implemented in UC7

- `QuantityLength.Add(QuantityLength other, LengthUnit targetUnit)`
- `QuantityLength.Add(double value, LengthUnit unit, LengthUnit targetUnit)`
- Service overloads for explicit target unit in `QuantityMeasurementService`
- Validation for invalid/unsupported target unit values
- UC7-focused tests added to `UnitAdditionTests.cs`

---

## 🧠 Concepts Demonstrated in UC7

- Explicit parameterized result unit
- Commutativity with a fixed target unit
- Mathematical equivalence across different target representations
- Edge-case support (zero, negative, cross-scale values)

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
   ├── UnitConversionTests.cs
   └── UnitAdditionTests.cs
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

UC6 Examples:

Input: add(Quantity(1.0, Feet), Quantity(12.0, Inches))
Output: Quantity(2.0, Feet)

Input: add(12.0, Inches, 1.0, Feet, Inches)
Output: Quantity(24.0, Inches)

Input: add(1.0, Yards, 3.0, Feet, Yards)
Output: Quantity(2.0, Yards)

UC7 Examples:

Input: add(Quantity(1.0, Feet), Quantity(12.0, Inches), Feet)
Output: Quantity(2.0, Feet)

Input: add(Quantity(1.0, Feet), Quantity(12.0, Inches), Inches)
Output: Quantity(24.0, Inches)

Input: add(Quantity(1.0, Feet), Quantity(12.0, Inches), Yards)
Output: Quantity(0.666..., Yards)

Input: add(Quantity(36.0, Inches), Quantity(1.0, Yards), Feet)
Output: Quantity(6.0, Feet)

---

## 🧪 How to Run Unit Tests

From solution root directory:

dotnet test

All test cases must pass before merging into develop or main branch.

---