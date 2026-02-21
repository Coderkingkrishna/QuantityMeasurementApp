using System;
using QuantityMeasurementApp.Core.Models;
using QuantityMeasurementApp.Core.Services;

namespace QuantityMeasurementApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var firstFeetMeasurement = new Feet(1.0);
                var secondFeetMeasurement = new Feet(1.0);
                var firstInchesMeasurement = new Inches(12.0);
                var secondInchesMeasurement = new Inches(12.0);

                var quantityMeasurementService = new QuantityMeasurementService();

                bool areFeetEqual = quantityMeasurementService.AreEqual(
                    firstFeetMeasurement,
                    secondFeetMeasurement
                );
                bool areInchesEqual = quantityMeasurementService.AreEqual(
                    firstInchesMeasurement,
                    secondInchesMeasurement
                );

                Console.WriteLine(
                    $"Input: {firstFeetMeasurement.Value} ft and {secondFeetMeasurement.Value} ft"
                );
                Console.WriteLine($"Output: Equal ({areFeetEqual})");

                Console.WriteLine(
                    $"Input: {firstInchesMeasurement.Value} in and {secondInchesMeasurement.Value} in"
                );
                Console.WriteLine($"Output: Equal ({areInchesEqual})");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"An error occurred: {exception.Message}");
            }
        }
    }
}
