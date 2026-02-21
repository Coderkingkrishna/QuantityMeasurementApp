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
- Static equality methods in `QuantityMeasurementService`
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

QuantityMeasurementApp.sln

src/
 └── QuantityMeasurementApp
      ├── Program.cs
      ├── Models/
      │     ├── Feet.cs
   │     ├── Inches.cs
   │     ├── LengthUnit.cs
   │     └── Quantity.cs
      └── Services/
            └── QuantityMeasurementService.cs

tests/
 └── QuantityMeasurementApp.Tests
      ├── FeetTests.cs
   ├── InchesTests.cs
   └── QuantityTests.cs

---

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

---

## 🧪 How to Run Unit Tests

From solution root directory:

dotnet test

All test cases must pass before merging into develop or main branch.

---