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

        /// <summary>
        /// Compares two QuantityLength measurements for equality, supporting cross-unit comparison.
        /// This method eliminates code duplication and supports length units.
        /// </summary>
        /// <param name="firstMeasurement">The first measurement</param>
        /// <param name="secondMeasurement">The second measurement to compare .</param>
        /// <returns>True if the measurements are equal after unit conversion; otherwise, false.</returns>
        public bool AreEqual(QuantityLength firstMeasurement, QuantityLength secondMeasurement)
        {
            return firstMeasurement.Equals(secondMeasurement);
        }

        /// <summary>
        /// Converts a measurement from a source unit to a target unit using the QuantityLength class for conversion.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="sourceUnit">The unit of the value to convert.</param>
        public double Convert(double value, LengthUnit sourceUnit, LengthUnit targetUnit)
        {
            var quantity = new QuantityLength(value, sourceUnit);
            return quantity.ConvertTo(targetUnit);
        }
    }
}
