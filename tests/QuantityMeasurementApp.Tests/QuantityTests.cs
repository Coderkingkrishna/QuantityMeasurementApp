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

        // ==================== CROSS-UNIT EQUALITY TESTS ====================

        /// <summary>
        /// Test to verify that Quantity(1.0, Feet) equals Quantity(12.0, Inches).
        /// Tests: Cross-unit comparison with unit conversion (1 foot = 12 inches).
        /// </summary>
        [TestMethod]
        public void TestEquality_FeetToInches_EquivalentValue()
        {
            var feet = new QuantityLength(1.0, LengthUnit.Feet);
            var inches = new QuantityLength(12.0, LengthUnit.Inches);

            Assert.IsTrue(feet.Equals(inches));
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
        /// Test to verify that two Quantity instances with different inches values are not considered equal.
        /// Tests: Quantity(1.0, Inches) does not equal Quantity(2.0, Inches).
        /// </summary>
        [TestMethod]
        public void TestEquality_InchToInch_DifferentValue()
        {
            var first = new QuantityLength(1.0, LengthUnit.Inches);
            var second = new QuantityLength(2.0, LengthUnit.Inches);

            Assert.IsFalse(first.Equals(second));
        }

        /// <summary>
        /// Test to verify that Quantity(1.0, Feet) does not equal Quantity(1.0, Inches).
        /// Tests: Cross-unit comparison with inequivalent values.
        /// </summary>
        [TestMethod]
        public void TestEquality_FeetToInches_DifferentValue()
        {
            var feet = new QuantityLength(1.0, LengthUnit.Feet);
            var inches = new QuantityLength(1.0, LengthUnit.Inches);

            Assert.IsFalse(feet.Equals(inches));
        }

        // ==================== REFLEXIVE PROPERTY TESTS ====================

        /// <summary>
        /// Test to verify that a Quantity object equals itself (reflexive property).
        /// Tests: a.Equals(a) must return true.
        /// </summary>
        [TestMethod]
        public void TestEquality_SameReference()
        {
            var first = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsTrue(first.Equals(first));
        }

        // ==================== NULL COMPARISON TESTS ====================

        /// <summary>
        /// Test to verify that a Quantity object is not equal to null.
        /// Tests: a.Equals(null) must return false.
        /// </summary>
        [TestMethod]
        public void TestEquality_NullComparison()
        {
            var first = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsFalse(first.Equals(null));
        }

        /// <summary>
        /// Test to verify that object.Equals(null) returns false for Quantity objects.
        /// Tests: (object)a.Equals(null) must return false.
        /// </summary>
        [TestMethod]
        public void TestEquality_ObjectNullComparison()
        {
            object first = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsFalse(first.Equals(null));
        }

        // ==================== TYPE SAFETY TESTS ====================

        /// <summary>
        /// Test to verify that a Quantity object is not equal to an object of a different type.
        /// Tests: Type safety - prevents invalid comparisons.
        /// </summary>
        [TestMethod]
        public void TestEquality_DifferentType()
        {
            var quantity = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsFalse(quantity.Equals("1.0"));
            Assert.IsFalse(quantity.Equals(1.0));
            Assert.IsFalse(quantity.Equals(new object()));
        }

        // ==================== SYMMETRY PROPERTY TESTS ====================

        /// <summary>
        /// Test to verify the symmetry property of equality: if a.equals(b) then b.equals(a).
        /// Tests: Quantity(1.0, Feet).Equals(Quantity(12.0, Inches)) implies
        ///        Quantity(12.0, Inches).Equals(Quantity(1.0, Feet)).
        /// </summary>
        [TestMethod]
        public void TestEquality_SymmetryFeetToInches()
        {
            var feet = new QuantityLength(1.0, LengthUnit.Feet);
            var inches = new QuantityLength(12.0, LengthUnit.Inches);

            Assert.IsTrue(feet.Equals(inches));
            Assert.IsTrue(inches.Equals(feet)); // Test symmetry
        }

        /// <summary>
        /// Test to verify the symmetry property for same-unit comparisons.
        /// </summary>
        [TestMethod]
        public void TestEquality_SymmetrySameUnit()
        {
            var first = new QuantityLength(5.0, LengthUnit.Feet);
            var second = new QuantityLength(5.0, LengthUnit.Feet);

            Assert.IsTrue(first.Equals(second));
            Assert.IsTrue(second.Equals(first)); // Test symmetry
        }

        // ==================== TRANSITIVITY PROPERTY TESTS ====================

        /// <summary>
        /// Test to verify the transitivity property: if a.equals(b) and b.equals(c) then a.equals(c).
        /// Tests: Quantity(1.0, Feet) equals Quantity(12.0, Inches) equals Quantity(1.0, Feet).
        /// </summary>
        [TestMethod]
        public void TestEquality_Transitivity()
        {
            var feet1 = new QuantityLength(1.0, LengthUnit.Feet);
            var inches12 = new QuantityLength(12.0, LengthUnit.Inches);
            var feet1Again = new QuantityLength(1.0, LengthUnit.Feet);

            // a equals b
            Assert.IsTrue(feet1.Equals(inches12));
            // b equals c
            Assert.IsTrue(inches12.Equals(feet1Again));
            // Therefore, a must equal c (transitivity)
            Assert.IsTrue(feet1.Equals(feet1Again));
        }

        // ==================== CONSISTENCY PROPERTY TESTS ====================

        /// <summary>
        /// Test to verify the consistency property: multiple calls to equals return the same result.
        /// </summary>
        [TestMethod]
        public void TestEquality_Consistency()
        {
            var first = new QuantityLength(1.0, LengthUnit.Feet);
            var second = new QuantityLength(12.0, LengthUnit.Inches);

            // Multiple calls should return the same result
            bool result1 = first.Equals(second);
            bool result2 = first.Equals(second);
            bool result3 = first.Equals(second);

            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Assert.IsTrue(result3);
            Assert.AreEqual(result1, result2);
            Assert.AreEqual(result2, result3);
        }

        // ==================== HASH CODE TESTS ====================

        /// <summary>
        /// Test to verify that equal Quantity objects have the same hash code.
        /// Tests: If a.Equals(b) then a.GetHashCode() must equal b.GetHashCode().
        /// </summary>
        [TestMethod]
        public void TestHashCode_EqualObjectsHaveSameHashCode()
        {
            var feet = new QuantityLength(1.0, LengthUnit.Feet);
            var inches = new QuantityLength(12.0, LengthUnit.Inches);

            Assert.IsTrue(feet.Equals(inches));
            Assert.AreEqual(feet.GetHashCode(), inches.GetHashCode());
        }

        /// <summary>
        /// Test to verify that equal same-unit objects have the same hash code.
        /// </summary>
        [TestMethod]
        public void TestHashCode_EqualSameUnitObjectsHaveSameHashCode()
        {
            var first = new QuantityLength(5.0, LengthUnit.Feet);
            var second = new QuantityLength(5.0, LengthUnit.Feet);

            Assert.IsTrue(first.Equals(second));
            Assert.AreEqual(first.GetHashCode(), second.GetHashCode());
        }

        /// <summary>
        /// Test to verify that different Quantity objects have different hash codes (usually).
        /// Note: Hash code collisions are possible but unlikely for different values.
        /// </summary>
        [TestMethod]
        public void TestHashCode_DifferentObjectsHaveDifferentHashCode()
        {
            var feet1 = new QuantityLength(1.0, LengthUnit.Feet);
            var feet2 = new QuantityLength(2.0, LengthUnit.Feet);

            Assert.IsFalse(feet1.Equals(feet2));
            // Note: Hash code collisions are theoretically possible, but unlikely
            Assert.AreNotEqual(feet1.GetHashCode(), feet2.GetHashCode());
        }

        // ==================== EDGE CASE TESTS ====================

        /// <summary>
        /// Test to verify that zero values are handled correctly.
        /// Tests: Quantity(0.0, Feet) equals Quantity(0.0, Inches).
        /// </summary>
        [TestMethod]
        public void TestEquality_ZeroValues()
        {
            var zeroFeet = new QuantityLength(0.0, LengthUnit.Feet);
            var zeroInches = new QuantityLength(0.0, LengthUnit.Inches);

            Assert.IsTrue(zeroFeet.Equals(zeroInches));
        }

        /// <summary>
        /// Test to verify that negative values are handled correctly.
        /// Tests: Quantity(-1.0, Feet) equals Quantity(-12.0, Inches).
        /// </summary>
        [TestMethod]
        public void TestEquality_NegativeValues()
        {
            var negativeFeet = new QuantityLength(-1.0, LengthUnit.Feet);
            var negativeInches = new QuantityLength(-12.0, LengthUnit.Inches);

            Assert.IsTrue(negativeFeet.Equals(negativeInches));
        }

        /// <summary>
        /// Test to verify that very small values are handled correctly with floating-point precision.
        /// Tests: Quantity(0.0001, Feet) equals Quantity(0.0012, Inches) to within tolerance.
        /// </summary>
        [TestMethod]
        public void TestEquality_SmallValues()
        {
            var smallFeet = new QuantityLength(0.0001, LengthUnit.Feet);
            var smallInches = new QuantityLength(0.0012, LengthUnit.Inches);

            Assert.IsTrue(smallFeet.Equals(smallInches));
        }

        /// <summary>
        /// Test to verify that large values are handled correctly.
        /// Tests: Quantity(1000.0, Feet) equals Quantity(12000.0, Inches).
        /// </summary>
        [TestMethod]
        public void TestEquality_LargeValues()
        {
            var largeFeet = new QuantityLength(1000.0, LengthUnit.Feet);
            var largeInches = new QuantityLength(12000.0, LengthUnit.Inches);

            Assert.IsTrue(largeFeet.Equals(largeInches));
        }

        // ==================== BACKWARD COMPATIBILITY TESTS ====================

        /// <summary>
        /// Test to verify that the Quantity class can be used with the QuantityMeasurementService.
        /// Tests: Service.AreEqual(Quantity, Quantity) returns correct results.
        /// </summary>
        [TestMethod]
        public void TestServiceIntegration_AreEqualWithQuantity()
        {
            var service = new QuantityMeasurementService();
            var feet = new QuantityLength(1.0, LengthUnit.Feet);
            var inches = new QuantityLength(12.0, LengthUnit.Inches);

            Assert.IsTrue(service.AreEqual(feet, inches));
        }

        /// <summary>
        /// Test to verify that the original Feet and Inches classes still work with the service (backward compatibility).
        /// </summary>
        [TestMethod]
        public void TestServiceIntegration_BackwardCompatibilityWithFeetAndInches()
        {
            var service = new QuantityMeasurementService();
            var feet1 = new Feet(1.0);
            var feet2 = new Feet(1.0);
            var inches1 = new Inches(12.0);
            var inches2 = new Inches(12.0);

            Assert.IsTrue(service.AreEqual(feet1, feet2));
            Assert.IsTrue(service.AreEqual(inches1, inches2));
        }

        // ==================== TOSTRING TESTS ====================

        /// <summary>
        /// Test to verify that the ToString method returns a proper string representation.
        /// </summary>
        [TestMethod]
        public void TestToString_ProperRepresentation()
        {
            var quantity = new QuantityLength(1.0, LengthUnit.Feet);
            string result = quantity.ToString();

            Assert.AreEqual("Quantity(1, Feet)", result);
        }
    }
}
