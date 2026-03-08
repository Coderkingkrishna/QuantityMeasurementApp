using System;
using QuantityMeasurementApp.Core.Models;
using QuantityMeasurementApp.Core.Services;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Console entry point for UC10 demonstrations.
    /// Shows that one generic <see cref="Quantity{U}"/> API supports multiple categories
    /// (length and weight) through shared equality, conversion, and addition flows.
    /// </summary>
    /// <remarks>
    /// Demonstration methods are generic to avoid category-specific duplication in orchestration code.
    /// </remarks>
    internal class Program
    {
        /// <summary>
        /// Demonstrates equality comparison between two quantities in the same category.
        /// </summary>
        /// <typeparam name="U">The enum unit type.</typeparam>
        /// <param name="service">The measurement service facade.</param>
        /// <param name="first">The first quantity.</param>
        /// <param name="second">The second quantity.</param>
        private static void DemonstrateEquality<U>(
            QuantityMeasurementService service,
            Quantity<U> first,
            Quantity<U> second
        )
            where U : struct, Enum
        {
            Console.WriteLine(
                $"Input: {first} and {second} -> Output: {service.AreEqual(first, second)}"
            );
        }

        /// <summary>
        /// Demonstrates unit conversion for a quantity.
        /// </summary>
        /// <typeparam name="U">The enum unit type.</typeparam>
        /// <param name="service">The measurement service facade.</param>
        /// <param name="source">The source quantity.</param>
        /// <param name="targetUnit">The target unit.</param>
        private static void DemonstrateConversion<U>(
            QuantityMeasurementService service,
            Quantity<U> source,
            U targetUnit
        )
            where U : struct, Enum
        {
            Console.WriteLine(
                $"Input: {source}.ConvertTo({targetUnit}) -> Output: {source.ConvertTo(targetUnit)}"
            );
        }

        /// <summary>
        /// Demonstrates addition of two quantities with an explicit target unit.
        /// </summary>
        /// <typeparam name="U">The enum unit type.</typeparam>
        /// <param name="service">The measurement service facade.</param>
        /// <param name="first">The first quantity.</param>
        /// <param name="second">The second quantity.</param>
        /// <param name="targetUnit">The target unit for the sum.</param>
        private static void DemonstrateAddition<U>(
            QuantityMeasurementService service,
            Quantity<U> first,
            Quantity<U> second,
            U targetUnit
        )
            where U : struct, Enum
        {
            Console.WriteLine(
                $"Input: {first}.Add({second}, {targetUnit}) -> Output: {service.Add(first, second, targetUnit)}"
            );
        }

        /// <summary>
        /// Demonstrates subtraction of two quantities in the first operand unit.
        /// </summary>
        /// <typeparam name="U">The enum unit type.</typeparam>
        /// <param name="service">The measurement service facade.</param>
        /// <param name="first">The minuend quantity.</param>
        /// <param name="second">The subtrahend quantity.</param>
        private static void DemonstrateSubtraction<U>(
            QuantityMeasurementService service,
            Quantity<U> first,
            Quantity<U> second
        )
            where U : struct, Enum
        {
            Console.WriteLine(
                $"Input: {first}.Subtract({second}) -> Output: {service.Subtract(first, second)}"
            );
        }

        /// <summary>
        /// Demonstrates subtraction of two quantities in an explicit target unit.
        /// </summary>
        /// <typeparam name="U">The enum unit type.</typeparam>
        /// <param name="service">The measurement service facade.</param>
        /// <param name="first">The minuend quantity.</param>
        /// <param name="second">The subtrahend quantity.</param>
        /// <param name="targetUnit">The target unit for the difference.</param>
        private static void DemonstrateSubtraction<U>(
            QuantityMeasurementService service,
            Quantity<U> first,
            Quantity<U> second,
            U targetUnit
        )
            where U : struct, Enum
        {
            Console.WriteLine(
                $"Input: {first}.Subtract({second}, {targetUnit}) -> Output: {service.Subtract(first, second, targetUnit)}"
            );
        }

        /// <summary>
        /// Demonstrates division of two quantities returning a dimensionless ratio.
        /// </summary>
        /// <typeparam name="U">The enum unit type.</typeparam>
        /// <param name="service">The measurement service facade.</param>
        /// <param name="first">The dividend quantity.</param>
        /// <param name="second">The divisor quantity.</param>
        private static void DemonstrateDivision<U>(
            QuantityMeasurementService service,
            Quantity<U> first,
            Quantity<U> second
        )
            where U : struct, Enum
        {
            Console.WriteLine(
                $"Input: {first}.Divide({second}) -> Output: {service.Divide(first, second)}"
            );
        }

        /// <summary>
        /// Main application entry point for UC10-UC12 demonstrations.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        private static void Main(string[] args)
        {
            try
            {
                var quantityMeasurementService = new QuantityMeasurementService();

                Console.WriteLine("=== Length Operations ===");
                DemonstrateEquality(
                    quantityMeasurementService,
                    new Quantity<LengthUnit>(1.0, LengthUnit.Feet),
                    new Quantity<LengthUnit>(12.0, LengthUnit.Inches)
                );
                DemonstrateConversion(
                    quantityMeasurementService,
                    new Quantity<LengthUnit>(1.0, LengthUnit.Feet),
                    LengthUnit.Inches
                );
                DemonstrateAddition(
                    quantityMeasurementService,
                    new Quantity<LengthUnit>(1.0, LengthUnit.Feet),
                    new Quantity<LengthUnit>(12.0, LengthUnit.Inches),
                    LengthUnit.Feet
                );
                DemonstrateSubtraction(
                    quantityMeasurementService,
                    new Quantity<LengthUnit>(10.0, LengthUnit.Feet),
                    new Quantity<LengthUnit>(6.0, LengthUnit.Inches)
                );
                DemonstrateSubtraction(
                    quantityMeasurementService,
                    new Quantity<LengthUnit>(10.0, LengthUnit.Feet),
                    new Quantity<LengthUnit>(6.0, LengthUnit.Inches),
                    LengthUnit.Inches
                );
                DemonstrateDivision(
                    quantityMeasurementService,
                    new Quantity<LengthUnit>(24.0, LengthUnit.Inches),
                    new Quantity<LengthUnit>(2.0, LengthUnit.Feet)
                );

                Console.WriteLine();
                Console.WriteLine("=== Weight Operations ===");
                DemonstrateEquality(
                    quantityMeasurementService,
                    new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram),
                    new Quantity<WeightUnit>(1000.0, WeightUnit.Gram)
                );
                DemonstrateConversion(
                    quantityMeasurementService,
                    new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram),
                    WeightUnit.Gram
                );
                DemonstrateAddition(
                    quantityMeasurementService,
                    new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram),
                    new Quantity<WeightUnit>(1000.0, WeightUnit.Gram),
                    WeightUnit.Kilogram
                );
                DemonstrateSubtraction(
                    quantityMeasurementService,
                    new Quantity<WeightUnit>(10.0, WeightUnit.Kilogram),
                    new Quantity<WeightUnit>(5000.0, WeightUnit.Gram)
                );
                DemonstrateSubtraction(
                    quantityMeasurementService,
                    new Quantity<WeightUnit>(10.0, WeightUnit.Kilogram),
                    new Quantity<WeightUnit>(5000.0, WeightUnit.Gram),
                    WeightUnit.Gram
                );
                DemonstrateDivision(
                    quantityMeasurementService,
                    new Quantity<WeightUnit>(10.0, WeightUnit.Kilogram),
                    new Quantity<WeightUnit>(5.0, WeightUnit.Kilogram)
                );

                Console.WriteLine();
                Console.WriteLine("=== Volume Operations ===");
                DemonstrateEquality(
                    quantityMeasurementService,
                    new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre),
                    new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre)
                );
                DemonstrateConversion(
                    quantityMeasurementService,
                    new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon),
                    VolumeUnit.Litre
                );
                DemonstrateAddition(
                    quantityMeasurementService,
                    new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre),
                    new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon),
                    VolumeUnit.Millilitre
                );
                DemonstrateSubtraction(
                    quantityMeasurementService,
                    new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre),
                    new Quantity<VolumeUnit>(500.0, VolumeUnit.Millilitre)
                );
                DemonstrateSubtraction(
                    quantityMeasurementService,
                    new Quantity<VolumeUnit>(5.0, VolumeUnit.Litre),
                    new Quantity<VolumeUnit>(2.0, VolumeUnit.Litre),
                    VolumeUnit.Millilitre
                );
                DemonstrateDivision(
                    quantityMeasurementService,
                    new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre),
                    new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre)
                );
            }
            catch (Exception exception)
            {
                Console.WriteLine($"An error occurred: {exception.Message}");
            }
        }
    }
}
