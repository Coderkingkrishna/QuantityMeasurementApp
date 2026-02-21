# QuantityMeasurementApp

## 📌 Project Overview

QuantityMeasurementApp is a domain-driven C# application that compares quantities of measurement. 

The project is being developed incrementally using a hybrid approach of:

- Design-Develop-Test (DDT)
- Test-Driven Development (TDD)

Each use case is implemented within a strictly defined scope to maintain clean architecture and scalability.

---

## 🚀 Use Case 1 (UC1) – Feet Measurement Equality

### Description

UC1 implements equality comparison between two numerical values measured in feet.

The system ensures:

- Accurate floating-point comparison
- Value-based equality
- Type safety
- Null safety
- Clean object-oriented design

---

## ✅ Features Implemented

- Immutable `Feet` value object
- Proper override of `Equals()` and `GetHashCode()`
- Implementation of `IEquatable<Feet>`
- `QuantityMeasurementService` for comparison logic
- MSTest unit testing
- Exception-safe program execution

---

## 🧠 Concepts Demonstrated

- Equality Contract:
  - Reflexive
  - Symmetric
  - Transitive
  - Consistent
  - Null Handling
- SOLID Principles
- Encapsulation
- Immutability
- Floating-point comparison using `double.Compare()`
- Clean architecture separation

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
      │     └── Feet.cs
      └── Services/
            └── QuantityMeasurementService.cs

tests/
 └── QuantityMeasurementApp.Tests
      └── FeetTests.cs

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

Input: 1.0 ft and 1.0 ft  
Output: Equal (True)

---

## 🧪 How to Run Unit Tests

From the solution root directory:

dotnet test

All test cases must pass before merging into develop or main branch.

---