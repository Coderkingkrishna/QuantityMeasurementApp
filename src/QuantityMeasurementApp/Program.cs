using System;
using QuantityMeasurementApp.Core.Models;
using QuantityMeasurementApp.Core.Services;

namespace QuantityMeasurementApp
{
    ///<summary>
    /// The Program class contains the Main method, which serves as the entry point of the application. It demonstrates the usage of the QuantityMeasurementService to compare measurements in feet and inches for equality.
    /// The Main method creates sample measurements in feet and inches, compares them using the QuantityMeasurementService, and displays the results of the comparisons.
    /// It also demonstrates the new QuantityLength class that supports cross-unit comparisons.
    /// The program includes error handling to catch any exceptions that may occur during the execution and display an appropriate error message.
    /// <code>
    /// var service = new QuantityMeasurementService();
    /// var feet1 = new Feet(1.0);
    /// var feet2 = new Feet(1.0);
    /// bool areFeetEqual = service.AreEqual(feet1, feet2); // returns true
    /// var inches1 = new Inches(12.0);
    /// var inches2 = new Inches(12.0);
    /// bool areInchesEqual = service.AreEqual(inches1, inches2); // returns true
    ///
    /// // QuantityLength class usage
    /// var quantity1 = new QuantityLength(1.0, LengthUnit.Feet);
    /// var quantity2 = new QuantityLength(12.0, LengthUnit.Inches);
    /// bool areQuantitiesEqual = service.AreEqual(quantity1, quantity2); // returns true
    /// </code>
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            // to demonstrate the usage of QuantityMeasurementService to compare feet and inches measurements for equality
            try
            {
                // Create sample measurements in feet and inches using the Feet and Inches classes (UC1 and UC2)
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

                // Display the results of the comparisons from UC1 and UC2
                Console.WriteLine("=== UC1 : FeetClasses ===");
                Console.WriteLine(
                    $"Input: Feet({firstFeetMeasurement.Value}) and Feet({secondFeetMeasurement.Value})"
                );
                Console.WriteLine($"Output: Equal ({areFeetEqual})");
                Console.WriteLine();
                Console.WriteLine("=== UC2 : InchesClasses ===");
                Console.WriteLine(
                    $"Input: Inches({firstInchesMeasurement.Value}) and Inches({secondInchesMeasurement.Value})"
                );
                Console.WriteLine($"Output: Equal ({areInchesEqual})");
                Console.WriteLine();

                // Demonstrate the new QuantityLength class (UC3)
                Console.WriteLine("=== UC3: QuantityLength Class with Cross-Unit===");

                // Create QuantityLength instances with different units
                var quantity1Feet = new QuantityLength(1.0, LengthUnit.Feet);
                var quantity12Inches = new QuantityLength(12.0, LengthUnit.Inches);
                var quantity1Inch = new QuantityLength(1.0, LengthUnit.Inches);
                var quantity2Feet = new QuantityLength(2.0, LengthUnit.Feet);

                // Example 1: 1 foot equals 12 inches (cross-unit comparison)
                bool isFeetEqualToInches = quantityMeasurementService.AreEqual(
                    quantity1Feet,
                    quantity12Inches
                );
                Console.WriteLine($"Input: Quantity(1.0, Feet) and Quantity(12.0, Inches)");
                Console.WriteLine($"Output: Equal ({isFeetEqualToInches})");
                Console.WriteLine();

                // Example 2: 1 inch equals 1 inch (same unit comparison)
                var anotherInch = new QuantityLength(1.0, LengthUnit.Inches);
                bool isInchEqualToInch = quantityMeasurementService.AreEqual(
                    quantity1Inch,
                    anotherInch
                );
                Console.WriteLine($"Input: Quantity(1.0, Inches) and Quantity(1.0, Inches)");
                Console.WriteLine($"Output: Equal ({isInchEqualToInch})");
                Console.WriteLine();

                // Example 3: 1 foot does not equal 2 feet
                bool isDifferentFeet = quantityMeasurementService.AreEqual(
                    quantity1Feet,
                    quantity2Feet
                );
                Console.WriteLine($"Input: Quantity(1.0, Feet) and Quantity(2.0, Feet)");
                Console.WriteLine($"Output: Equal ({isDifferentFeet})");
            }
            // catch any exceptions that may occur during the execution of the program and display an error message
            catch (Exception exception)
            {
                Console.WriteLine($"An error occurred: {exception.Message}");
            }
        }
    }
}
