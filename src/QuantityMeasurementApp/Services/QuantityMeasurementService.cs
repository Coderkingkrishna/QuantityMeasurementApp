using QuantityMeasurementApp.Core.Models;

namespace QuantityMeasurementApp.Core.Services
{
    public class QuantityMeasurementService
    {
        public bool AreEqual(Feet firstMeasurement, Feet secondMeasurement)
        {
            return firstMeasurement.Equals(secondMeasurement);
        }
    }
}
