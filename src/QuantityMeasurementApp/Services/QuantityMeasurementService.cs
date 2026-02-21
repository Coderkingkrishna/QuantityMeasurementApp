//to add model for feet measurement
using QuantityMeasurementApp.Core.Models;

namespace QuantityMeasurementApp.Core.Services
{
    ///<summary>
    /// The QuantityMeasurementService class provides methods to compare measurements in feet and inches for equality.
    /// It contains methods that take two measurements as input and return true if they are equal, otherwise false.
    /// The service uses the Equals method defined in the Feet and Inches classes to perform the comparison.
    /// <code>
    /// var service = new QuantityMeasurementService();
    /// var feet1 = new Feet(1.0);
    /// var feet2 = new Feet(1.0);
    /// bool areFeetEqual = service.AreEqual(feet1, feet2); // returns true
    /// var inches1 = new Inches(12.0);
    /// var inches2 = new Inches(12.0);
    /// bool areInchesEqual = service.AreEqual(inches1, inches2); // returns true
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
    }
}
