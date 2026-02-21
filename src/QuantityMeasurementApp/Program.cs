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
                var firstMeasurement = new Feet(1.0);
                var secondMeasurement = new Feet(1.0);

                var quantityMeasurementService = new QuantityMeasurementService();

                bool areEqual = quantityMeasurementService.AreEqual(
                    firstMeasurement,
                    secondMeasurement
                );

                Console.WriteLine(
                    $"Input: {firstMeasurement.Value} ft and {secondMeasurement.Value} ft"
                );
                Console.WriteLine($"Output: Equal ({areEqual})");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"An error occurred: {exception.Message}");
            }
        }
    }
}
