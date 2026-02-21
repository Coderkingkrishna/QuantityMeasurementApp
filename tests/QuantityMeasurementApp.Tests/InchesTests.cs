using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Core.Models;

namespace QuantityMeasurementApp.Tests
{
    ///<summary>
    /// The InchesTests class contains unit tests for the Inches class to verify the equality comparison logic
    /// The tests include scenarios for comparing Inches instances with the same value, different values, null, same reference, and different types. Each test method uses assertions to validate the expected outcomes of the equality comparisons.
    /// <code>
    /// [TestMethod]
    /// public void TestEquality_SameValue()
    /// {
    ///  ///     var first = new Inches(1.0);
    /// var second = new Inches(1.0);
    /// Assert.IsTrue(first.Equals(second));
    /// /// }
    /// </code>
    /// </summary>
    [TestClass]
    public class InchesTests
    {
        // Test to verify that two Inches instances with the same value are considered equal
        [TestMethod]
        public void TestEquality_SameValue()
        {
            var first = new Inches(1.0);
            var second = new Inches(1.0);

            Assert.IsTrue(first.Equals(second));
        }

        // Test to verify that two Inches instances with different values are not considered equal
        [TestMethod]
        public void TestEquality_DifferentValue()
        {
            var first = new Inches(1.0);
            var second = new Inches(2.0);

            Assert.IsFalse(first.Equals(second));
        }

        // Test to verify that an Inches instance is not considered equal to null
        [TestMethod]
        public void TestEquality_NullComparison()
        {
            var first = new Inches(1.0);

            Assert.IsFalse(first.Equals(null));
        }

        // Test to verify that an Inches instance is considered equal to itself (same reference)
        [TestMethod]
        public void TestEquality_SameReference()
        {
            var first = new Inches(1.0);

            Assert.IsTrue(first.Equals(first));
        }

        // Test to verify that an Inches instance is not considered equal to an object of a different type
        [TestMethod]
        public void TestEquality_DifferentType()
        {
            var first = new Inches(1.0);

            Assert.IsFalse(first.Equals("1.0"));
        }
    }
}
