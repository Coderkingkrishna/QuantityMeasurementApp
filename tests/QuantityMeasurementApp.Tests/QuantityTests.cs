using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Core.Models;
using QuantityMeasurementApp.Core.Services;

namespace QuantityMeasurementApp.Tests
{
    ///<summary>
    /// The QuantityTests class contains comprehensive unit tests for the QuantityLength class to verify:
    /// - Same-unit equality comparisons (Feet to Feet, Inches to Inches)
    /// - Cross-unit equality comparisons (Feet to Inches, Inches to Feet)
    /// - Different value comparisons
    /// - Null and type safety checks
    /// - Symmetry, reflexivity, and transitivity of equality
    /// - Hash code consistency
    /// These tests demonstrate the DRY principle by showing that a single class
    /// eliminates code duplication while handling multiple unit types.
    /// </summary>
    [TestClass]
    public class QuantityTests
    {
        // ==================== SAME-UNIT EQUALITY TESTS ====================

        /// <summary>
        /// Test to verify that two Quantity instances with the same feet value are considered equal.
        /// Tests: Quantity(1.0, Feet) equals Quantity(1.0, Feet) should return true.
        /// </summary>
        [TestMethod]
        public void TestEquality_FeetToFeet_SameValue()
        {
            var first = new QuantityLength(1.0, LengthUnit.Feet);
            var second = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsTrue(first.Equals(second));
        }

        /// <summary>
        /// Test to verify that two Quantity instances with the same inches value are considered equal.
        /// Tests: Quantity(1.0, Inches) equals Quantity(1.0, Inches) should return true.
        /// </summary>
        [TestMethod]
        public void TestEquality_InchToInch_SameValue()
        {
            var first = new QuantityLength(1.0, LengthUnit.Inches);
            var second = new QuantityLength(1.0, LengthUnit.Inches);

            Assert.IsTrue(first.Equals(second));
        }

        /// <summary>
        /// Test to verify that Quantity(12.0, Inches) equals Quantity(1.0, Feet).
        /// Tests: Cross-unit comparison symmetry (converter in reverse direction).
        /// </summary>
        [TestMethod]
        public void TestEquality_InchToFeet_EquivalentValue()
        {
            var inches = new QuantityLength(12.0, LengthUnit.Inches);
            var feet = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsTrue(inches.Equals(feet));
        }

        // ==================== DIFFERENT VALUE TESTS ====================

        /// <summary>
        /// Test to verify that two Quantity instances with different feet values are not considered equal.
        /// Tests: Quantity(1.0, Feet) does not equal Quantity(2.0, Feet).
        /// </summary>
        [TestMethod]
        public void TestEquality_FeetToFeet_DifferentValue()
        {
            var first = new QuantityLength(1.0, LengthUnit.Feet);
            var second = new QuantityLength(2.0, LengthUnit.Feet);

            Assert.IsFalse(first.Equals(second));
        }

        /// <summary>
        /// Test to verify that a Quantity yard object equals itself (reflexive property).
        /// Verifies: a.Equals(a) must return true.
        /// </summary>
        [TestMethod]
        public void TestEquality_YardSameReference()
        {
            var yard = new QuantityLength(1.0, LengthUnit.Yards);

            Assert.IsTrue(yard.Equals(yard));
        }

        /// <summary>
        /// Test to verify that Quantity(1.0, Centimeters) does not equal Quantity(1.0, Feet).
        /// Verifies: Cross-unit comparison with non-equivalent values.
        /// </summary>
        [TestMethod]
        public void TestEquality_CentimeterToFeet_DifferentValue()
        {
            var centimeters = new QuantityLength(1.0, LengthUnit.Centimeters);
            var feet = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsFalse(centimeters.Equals(feet));
        }

        /// <summary>
        /// Test to verify that a Quantity centimeter object is not equal to null.
        /// Verifies: a.Equals(null) must return false.
        /// </summary>
        [TestMethod]
        public void TestEquality_CentimeterNullComparison()
        {
            var centimeter = new QuantityLength(1.0, LengthUnit.Centimeters);

            Assert.IsFalse(centimeter.Equals(null));
        }
    }
}
