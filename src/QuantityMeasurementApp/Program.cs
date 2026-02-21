using System;
using QuantityMeasurementApp.Core.Models;
using QuantityMeasurementApp.Core.Services;

namespace QuantityMeasurementApp
{
    ///<summary>
    /// The Program class contains the Main method, which serves as the entry point of the application. It demonstrates the usage of the QuantityMeasurementService to compare measurements in feet and inches for equality.
    /// The Main method creates sample measurements in feet and inches, compares them using the QuantityMeasurement
    /// Service, and displays the results of the comparisons. It also includes error handling to catch any exceptions that may occur during the execution of the program and display an appropriate error message.
    /// <code>
    /// var service = new QuantityMeasurementService();
    /// var feet1 = new Feet(1.0);
    /// var feet2 = new Feet(1.0);
    /// bool areFeetEqual = service.AreEqual(feet1, feet2); // returns true
    /// var inches1 = new Inches(12.0);
    /// var inches2 = new Inches(12.0);
    /// bool areInchesEqual = service.AreEqual(inches1, inches2); //
    /// returns true
    /// </code>
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            // to demonstrate the usage of QuantityMeasurementService to compare feet and inches measurements for equality
            try
            {
                // Create sample measurements in feet and inches
                var firstFeetMeasurement = new Feet(1.0);
                var secondFeetMeasurement = new Feet(1.0);
                var firstInchesMeasurement = new Inches(12.0);
                var secondInchesMeasurement = new Inches(12.0);
                // create an object of QuantityMeasurementService for comparing the measurements
                var quantityMeasurementService = new QuantityMeasurementService();

                // Compare the feet and inches measurements for equality
                bool areFeetEqual = quantityMeasurementService.AreEqual(
                    firstFeetMeasurement,
                    secondFeetMeasurement
                );
                bool areInchesEqual = quantityMeasurementService.AreEqual(
                    firstInchesMeasurement,
                    secondInchesMeasurement
                );
                // Display the results of the comparisons
                Console.WriteLine(
                    $"Input: {firstFeetMeasurement.Value} ft and {secondFeetMeasurement.Value} ft"
                );
                Console.WriteLine($"Output: Equal ({areFeetEqual})");

                Console.WriteLine(
                    $"Input: {firstInchesMeasurement.Value} in and {secondInchesMeasurement.Value} in"
                );
                Console.WriteLine($"Output: Equal ({areInchesEqual})");
            }
            // catch any exceptions that may occur during the execution of the program and display an error message
            catch (Exception exception)
            {
                Console.WriteLine($"An error occurred: {exception.Message}");
            }
        }
    }
}
