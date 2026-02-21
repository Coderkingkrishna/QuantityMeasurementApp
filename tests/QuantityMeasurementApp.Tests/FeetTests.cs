using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Core.Models;

namespace QuantityMeasurementApp.Tests
{
    ///<summary>
    /// The FeetTests class contains unit tests for the Feet class to verify the equality comparison logic
    /// The tests include scenarios for comparing Feet instances with the same value, different values, null, same reference, and different types. Each test method uses assertions to validate the expected outcomes of the equality comparisons.
    /// <code>
    /// [TestMethod]
    /// public void TestEquality_SameValue()
    /// {
    ///   ///     var first = new Feet(1.0);
    ///   var second = new Feet(1.0);
    ///   Assert.IsTrue(first.Equals(second));
    /// /// }
    /// </code>
    /// </summary>
    // Unit tests for the Feet class to verify the equality comparison logic
    [TestClass]
    public class FeetTests
    {
        // Test to verify that two Feet instances with the same value are considered equal
        [TestMethod]
        public void TestEquality_SameValue()
        {
            var first = new Feet(1.0);
            var second = new Feet(1.0);

            Assert.IsTrue(first.Equals(second));
        }

        // Test to verify that two Feet instances with different values are not considered equal
        [TestMethod]
        public void TestEquality_DifferentValue()
        {
            var first = new Feet(1.0);
            var second = new Feet(2.0);

            Assert.IsFalse(first.Equals(second));
        }

        // Test to verify that a Feet instance is not considered equal to null
        [TestMethod]
        public void TestEquality_NullComparison()
        {
            var first = new Feet(1.0);

            Assert.IsFalse(first.Equals(null));
        }

        // Test to verify that a Feet instance is considered equal to itself (same reference)
        [TestMethod]
        public void TestEquality_SameReference()
        {
            var first = new Feet(1.0);

            Assert.IsTrue(first.Equals(first));
        }

        // Test to verify that a Feet instance is not considered equal to an object of a different type
        [TestMethod]
        public void TestEquality_DifferentType()
        {
            var first = new Feet(1.0);

            Assert.IsFalse(first.Equals("1.0"));
        }
    }
}
