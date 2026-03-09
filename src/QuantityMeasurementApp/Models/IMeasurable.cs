namespace QuantityMeasurementApp.Core.Models
{
    /// <summary>
    /// Functional capability contract used to indicate whether arithmetic operations are supported.
    /// </summary>
    /// <returns>True when arithmetic is supported; otherwise false.</returns>
    public delegate bool SupportsArithmetic();

    /// <summary>
    /// Common unit contract for UC10 generic quantity operations.
    /// Implementations define conversion to/from a category base unit.
    /// Optional arithmetic capability members are provided with non-breaking defaults.
    /// </summary>
    public interface IMeasurable
    {
        /// <summary>
        /// Gets linear conversion factor relative to the category base unit.
        /// </summary>
        /// <returns>The conversion factor.</returns>
        double GetConversionFactor();

        /// <summary>
        /// Converts a value in the current unit to base-unit value.
        /// </summary>
        /// <param name="value">The source-unit value.</param>
        /// <returns>The base-unit value.</returns>
        double ConvertToBaseUnit(double value);

        /// <summary>
        /// Converts a base-unit value to the current unit.
        /// </summary>
        /// <param name="baseValue">The base-unit value.</param>
        /// <returns>The target-unit value.</returns>
        double ConvertFromBaseUnit(double baseValue);

        /// <summary>
        /// Gets a display-friendly unit name.
        /// </summary>
        /// <returns>The unit name.</returns>
        string GetUnitName();

        /// <summary>
        /// Indicates whether arithmetic operations are supported for this measurable category.
        /// </summary>
        /// <returns>True for arithmetic-supporting categories; otherwise false.</returns>
        public bool SupportsArithmetic()
        {
            SupportsArithmetic supportsArithmetic = () => true;
            return supportsArithmetic();
        }

        /// <summary>
        /// Validates whether a named arithmetic operation is supported.
        /// Default behavior allows all operations.
        /// </summary>
        /// <param name="operation">The operation name being requested.</param>
        public void ValidateOperationSupport(string operation) { }
    }
}
