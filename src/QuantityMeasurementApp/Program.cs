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
                Console.WriteLine();

                // Demonstrate UC4: Yards and Centimeters
                Console.WriteLine("=== UC4: Extended Unit Support (Yards & Centimeters) ===");

                // Yard examples
                var quantity1Yard = new QuantityLength(1.0, LengthUnit.Yards);
                var quantity3Feet = new QuantityLength(3.0, LengthUnit.Feet);
                var quantity36Inches = new QuantityLength(36.0, LengthUnit.Inches);
                var quantity2Yards = new QuantityLength(2.0, LengthUnit.Yards);

                // Example 1: 1 yard equals 3 feet
                bool isYardEqualToFeet = quantityMeasurementService.AreEqual(
                    quantity1Yard,
                    quantity3Feet
                );
                Console.WriteLine($"Input: Quantity(1.0, Yards) and Quantity(3.0, Feet)");
                Console.WriteLine($"Output: Equal ({isYardEqualToFeet})");
                Console.WriteLine();

                // Example 2: 1 yard equals 36 inches
                bool isYardEqualToInches = quantityMeasurementService.AreEqual(
                    quantity1Yard,
                    quantity36Inches
                );
                Console.WriteLine($"Input: Quantity(1.0, Yards) and Quantity(36.0, Inches)");
                Console.WriteLine($"Output: Equal ({isYardEqualToInches})");
                Console.WriteLine();

                // Example 3: 2 yards equals 2 yards
                var anotherYard = new QuantityLength(2.0, LengthUnit.Yards);
                bool isYardEqualToYard = quantityMeasurementService.AreEqual(
                    quantity2Yards,
                    anotherYard
                );
                Console.WriteLine($"Input: Quantity(2.0, Yards) and Quantity(2.0, Yards)");
                Console.WriteLine($"Output: Equal ({isYardEqualToYard})");
                Console.WriteLine();

                // Centimeter examples
                var quantity2Centimeters = new QuantityLength(2.0, LengthUnit.Centimeters);
                var anotherCentimeter = new QuantityLength(2.0, LengthUnit.Centimeters);
                var quantity1Centimeter = new QuantityLength(1.0, LengthUnit.Centimeters);
                // 1 cm = 1/30.48 feet = 0.0328083989... feet = 0.393700787... inches
                var quantityInchesFromCm = new QuantityLength(
                    1.0 / 30.48 * 12.0,
                    LengthUnit.Inches
                );

                // Example 4: 2 cm equals 2 cm
                bool isCentimeterEqualToCentimeter = quantityMeasurementService.AreEqual(
                    quantity2Centimeters,
                    anotherCentimeter
                );
                Console.WriteLine(
                    $"Input: Quantity(2.0, Centimeters) and Quantity(2.0, Centimeters)"
                );
                Console.WriteLine($"Output: Equal ({isCentimeterEqualToCentimeter})");
                Console.WriteLine();

                // Example 5: 1 cm equals its equivalent in inches
                bool isCentimeterEqualToInches = quantityMeasurementService.AreEqual(
                    quantity1Centimeter,
                    quantityInchesFromCm
                );
                Console.WriteLine(
                    $"Input: Quantity(1.0, Centimeters) and Quantity({1.0 / 30.48 * 12.0}, Inches)"
                );
                Console.WriteLine($"Output: Equal ({isCentimeterEqualToInches})");
                Console.WriteLine();

                //Demonstrate UC5: Unit-to-Unit Conversion

                Console.WriteLine("=== UC5: Unit-to-Unit Conversion ===");
                // Example conversions using the Convert method in QuantityMeasurementService
                Console.WriteLine(
                    $"Input: convert(1.0, Feet, Inches) -> Output: {quantityMeasurementService.Convert(1.0, LengthUnit.Feet, LengthUnit.Inches)}"
                );
                Console.WriteLine(
                    $"Input: convert(3.0, Yards, Feet) -> Output: {quantityMeasurementService.Convert(3.0, LengthUnit.Yards, LengthUnit.Feet)}"
                );
                Console.WriteLine(
                    $"Input: convert(36.0, Inches, Yards) -> Output: {quantityMeasurementService.Convert(36.0, LengthUnit.Inches, LengthUnit.Yards)}"
                );
                Console.WriteLine(
                    $"Input: convert(1.0, Centimeters, Inches) -> Output: {quantityMeasurementService.Convert(1.0, LengthUnit.Centimeters, LengthUnit.Inches)}"
                );
                Console.WriteLine(
                    $"Input: convert(0.0, Feet, Inches) -> Output: {quantityMeasurementService.Convert(0.0, LengthUnit.Feet, LengthUnit.Inches)}"
                );
                Console.WriteLine();

                // Demonstrate UC6: Addition of Two Length Units
                Console.WriteLine("=== UC6: Addition of Two Length Units ===");
                var sumFeetAndInches = quantityMeasurementService.Add(
                    new QuantityLength(1.0, LengthUnit.Feet),
                    new QuantityLength(12.0, LengthUnit.Inches)
                );
                Console.WriteLine(
                    $"Input: add(Quantity(1.0, Feet), Quantity(12.0, Inches)) -> Output: {sumFeetAndInches}"
                );
                // Example of adding raw values with specified units
                var sumRawValues = quantityMeasurementService.Add(
                    2.0,
                    LengthUnit.Feet,
                    24.0,
                    LengthUnit.Inches
                );
                Console.WriteLine($"Input: add(2.0, Feet, 24.0, Inches) -> Output: {sumRawValues}");
            }
            // catch any exceptions that may occur during the execution of the program and display an error message
            catch (Exception exception)
            {
                Console.WriteLine($"An error occurred: {exception.Message}");
            }
        }
    }
}
