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
            }
            catch (Exception exception)
            {
                Console.WriteLine($"An error occurred: {exception.Message}");
            }
        }
    }
}
