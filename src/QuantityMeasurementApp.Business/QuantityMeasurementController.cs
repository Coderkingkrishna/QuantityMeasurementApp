using System;
using QuantityMeasurementApp.Models.DTOs;

namespace QuantityMeasurementApp.Business
{
    public class QuantityMeasurementController
    {
        private readonly IQuantityMeasurementService _service;

        public QuantityMeasurementController(IQuantityMeasurementService service)
        {
            _service = service;
        }

        public void DisplayResult(string message)
        {
            Console.WriteLine(message);
        }

        public void PerformCompare(QuantityDTO a, QuantityDTO b)
        {
            try
            {
                var result = _service.Compare(a, b);
                DisplayResult($"Comparison result: {result}");
            }
            catch (Exceptions.QuantityMeasurementException ex)
            {
                DisplayResult($"Error: {ex.Message}");
            }
        }
    }
}
