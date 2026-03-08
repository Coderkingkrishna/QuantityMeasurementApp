//to add model for feet measurement
using System;
using QuantityMeasurementApp.Core.Models;

namespace QuantityMeasurementApp.Core.Services
{
    ///<summary>
    /// The QuantityMeasurementService class provides methods to compare measurements in different units for equality.
    /// It contains methods that take two measurements as input and return true if they are equal, otherwise false.
    /// The service uses the Equals method defined in the Feet and Inches classes to perform the comparison.
    /// It also provides an AreEqual method that works with the QuantityLength class for length units.
    /// <code>
    /// var service = new QuantityMeasurementService();
    /// var feet1 = new Feet(1.0);
    /// var feet2 = new Feet(1.0);
    /// bool areFeetEqual = service.AreEqual(feet1, feet2); // returns true
    /// var inches1 = new Inches(12.0);
    /// var inches2 = new Inches(12.0);
    /// bool areInchesEqual = service.AreEqual(inches1, inches2); // returns true
    ///
    /// // Usage with QuantityLength class
    /// var quantity1 = new QuantityLength(1.0, LengthUnit.Feet);
    /// var quantity2 = new QuantityLength(12.0, LengthUnit.Inches);
    /// bool areQuantitiesEqual = service.AreEqual(quantity1, quantity2); // returns true
    /// </code>
    /// </summary>
    /// <remarks>
    /// In UC10 this service is a thin orchestrator over generic <see cref="Quantity{U}"/> behavior.
    /// Legacy overloads remain to preserve UC1/UC2 compatibility.
    /// </remarks>
    public class QuantityMeasurementService
    {
        //to compare two feet values for equality and return true if they are equal otherwise false
        public bool AreEqual(Feet firstMeasurement, Feet secondMeasurement)
        {
            return firstMeasurement.Equals(secondMeasurement);
        }

        //to compare two inches values for equality and return true if they are equal otherwise false
        public bool AreEqual(Inches firstMeasurement, Inches secondMeasurement)
        {
            return firstMeasurement.Equals(secondMeasurement);
        }

        public bool AreEqual<U>(Quantity<U> firstMeasurement, Quantity<U> secondMeasurement)
            where U : struct, Enum
        {
            return firstMeasurement.Equals(secondMeasurement);
        }

        /// <summary>
        /// Converts a primitive source value to the requested target unit.
        /// </summary>
        /// <typeparam name="U">The enum unit type.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <param name="sourceUnit">The source unit.</param>
        /// <param name="targetUnit">The target unit.</param>
        /// <returns>The converted value in the target unit.</returns>
        public double Convert<U>(double value, U sourceUnit, U targetUnit)
            where U : struct, Enum
        {
            var quantity = new Quantity<U>(value, sourceUnit);
            return quantity.ConvertTo(targetUnit).Value;
        }

        public Quantity<U> Add<U>(Quantity<U> firstMeasurement, Quantity<U> secondMeasurement)
            where U : struct, Enum
        {
            if (firstMeasurement is null)
                throw new ArgumentNullException(nameof(firstMeasurement));

            if (secondMeasurement is null)
                throw new ArgumentNullException(nameof(secondMeasurement));

            return firstMeasurement.Add(secondMeasurement);
        }

        public Quantity<U> Add<U>(
            Quantity<U> firstMeasurement,
            Quantity<U> secondMeasurement,
            U targetUnit
        )
            where U : struct, Enum
        {
            if (firstMeasurement is null)
                throw new ArgumentNullException(nameof(firstMeasurement));

            if (secondMeasurement is null)
                throw new ArgumentNullException(nameof(secondMeasurement));

            return firstMeasurement.Add(secondMeasurement, targetUnit);
        }

        /// <summary>
        /// Adds two primitive value/unit pairs and returns the result in the first operand unit.
        /// </summary>
        /// <typeparam name="U">The enum unit type.</typeparam>
        /// <param name="firstValue">First value.</param>
        /// <param name="firstUnit">First unit.</param>
        /// <param name="secondValue">Second value.</param>
        /// <param name="secondUnit">Second unit.</param>
        /// <returns>The summed quantity.</returns>
        public Quantity<U> Add<U>(double firstValue, U firstUnit, double secondValue, U secondUnit)
            where U : struct, Enum
        {
            var firstMeasurement = new Quantity<U>(firstValue, firstUnit);
            return firstMeasurement.Add(secondValue, secondUnit);
        }

        /// <summary>
        /// Adds two primitive value/unit pairs and returns the result in an explicit target unit.
        /// </summary>
        /// <typeparam name="U">The enum unit type.</typeparam>
        /// <param name="firstValue">First value.</param>
        /// <param name="firstUnit">First unit.</param>
        /// <param name="secondValue">Second value.</param>
        /// <param name="secondUnit">Second unit.</param>
        /// <param name="targetUnit">Target result unit.</param>
        /// <returns>The summed quantity in <paramref name="targetUnit"/>.</returns>
        public Quantity<U> Add<U>(
            double firstValue,
            U firstUnit,
            double secondValue,
            U secondUnit,
            U targetUnit
        )
            where U : struct, Enum
        {
            var firstMeasurement = new Quantity<U>(firstValue, firstUnit);
            return firstMeasurement.Add(secondValue, secondUnit, targetUnit);
        }

        /// <summary>
        /// Subtracts two quantities and returns the result in the first operand unit.
        /// </summary>
        /// <typeparam name="U">The enum unit type.</typeparam>
        /// <param name="firstMeasurement">The minuend quantity.</param>
        /// <param name="secondMeasurement">The subtrahend quantity.</param>
        /// <returns>The difference quantity in the first operand unit.</returns>
        public Quantity<U> Subtract<U>(Quantity<U> firstMeasurement, Quantity<U> secondMeasurement)
            where U : struct, Enum
        {
            if (firstMeasurement is null)
                throw new ArgumentNullException(nameof(firstMeasurement));

            if (secondMeasurement is null)
                throw new ArgumentNullException(nameof(secondMeasurement));

            return firstMeasurement.Subtract(secondMeasurement);
        }

        /// <summary>
        /// Subtracts two quantities and returns the result in an explicit target unit.
        /// </summary>
        /// <typeparam name="U">The enum unit type.</typeparam>
        /// <param name="firstMeasurement">The minuend quantity.</param>
        /// <param name="secondMeasurement">The subtrahend quantity.</param>
        /// <param name="targetUnit">The target unit for the difference.</param>
        /// <returns>The difference quantity in <paramref name="targetUnit"/>.</returns>
        public Quantity<U> Subtract<U>(
            Quantity<U> firstMeasurement,
            Quantity<U> secondMeasurement,
            U targetUnit
        )
            where U : struct, Enum
        {
            if (firstMeasurement is null)
                throw new ArgumentNullException(nameof(firstMeasurement));

            if (secondMeasurement is null)
                throw new ArgumentNullException(nameof(secondMeasurement));

            return firstMeasurement.Subtract(secondMeasurement, targetUnit);
        }

        /// <summary>
        /// Subtracts two primitive value/unit pairs and returns the result in first operand unit.
        /// </summary>
        /// <typeparam name="U">The enum unit type.</typeparam>
        /// <param name="firstValue">Minuend value.</param>
        /// <param name="firstUnit">Minuend unit.</param>
        /// <param name="secondValue">Subtrahend value.</param>
        /// <param name="secondUnit">Subtrahend unit.</param>
        /// <returns>The difference quantity.</returns>
        public Quantity<U> Subtract<U>(
            double firstValue,
            U firstUnit,
            double secondValue,
            U secondUnit
        )
            where U : struct, Enum
        {
            var firstMeasurement = new Quantity<U>(firstValue, firstUnit);
            return firstMeasurement.Subtract(new Quantity<U>(secondValue, secondUnit));
        }

        /// <summary>
        /// Subtracts two primitive value/unit pairs and returns the result in an explicit target unit.
        /// </summary>
        /// <typeparam name="U">The enum unit type.</typeparam>
        /// <param name="firstValue">Minuend value.</param>
        /// <param name="firstUnit">Minuend unit.</param>
        /// <param name="secondValue">Subtrahend value.</param>
        /// <param name="secondUnit">Subtrahend unit.</param>
        /// <param name="targetUnit">Target unit for the result.</param>
        /// <returns>The difference quantity in <paramref name="targetUnit"/>.</returns>
        public Quantity<U> Subtract<U>(
            double firstValue,
            U firstUnit,
            double secondValue,
            U secondUnit,
            U targetUnit
        )
            where U : struct, Enum
        {
            var firstMeasurement = new Quantity<U>(firstValue, firstUnit);
            return firstMeasurement.Subtract(new Quantity<U>(secondValue, secondUnit), targetUnit);
        }

        /// <summary>
        /// Divides one quantity by another and returns a dimensionless ratio.
        /// </summary>
        /// <typeparam name="U">The enum unit type.</typeparam>
        /// <param name="firstMeasurement">The dividend quantity.</param>
        /// <param name="secondMeasurement">The divisor quantity.</param>
        /// <returns>The scalar ratio.</returns>
        public double Divide<U>(Quantity<U> firstMeasurement, Quantity<U> secondMeasurement)
            where U : struct, Enum
        {
            if (firstMeasurement is null)
                throw new ArgumentNullException(nameof(firstMeasurement));

            if (secondMeasurement is null)
                throw new ArgumentNullException(nameof(secondMeasurement));

            return firstMeasurement.Divide(secondMeasurement);
        }

        /// <summary>
        /// Divides primitive value/unit pairs and returns a dimensionless ratio.
        /// </summary>
        /// <typeparam name="U">The enum unit type.</typeparam>
        /// <param name="firstValue">Dividend value.</param>
        /// <param name="firstUnit">Dividend unit.</param>
        /// <param name="secondValue">Divisor value.</param>
        /// <param name="secondUnit">Divisor unit.</param>
        /// <returns>The scalar ratio.</returns>
        public double Divide<U>(double firstValue, U firstUnit, double secondValue, U secondUnit)
            where U : struct, Enum
        {
            var firstMeasurement = new Quantity<U>(firstValue, firstUnit);
            var secondMeasurement = new Quantity<U>(secondValue, secondUnit);
            return firstMeasurement.Divide(secondMeasurement);
        }
    }
}
