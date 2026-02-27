using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Core.Models;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class QuantityWeightTests
    {
        [TestMethod]
        public void TestEquality_KilogramToKilogram_SameValue()
        {
            var first = new QuantityWeight(1.0, WeightUnit.Kilogram);
            var second = new QuantityWeight(1.0, WeightUnit.Kilogram);

            Assert.IsTrue(first.Equals(second));
        }

        [TestMethod]
        public void TestEquality_KilogramToGram_EquivalentValue()
        {
            var kilogram = new QuantityWeight(1.0, WeightUnit.Kilogram);
            var gram = new QuantityWeight(1000.0, WeightUnit.Gram);

            Assert.IsTrue(kilogram.Equals(gram));
            Assert.IsTrue(gram.Equals(kilogram));
        }

        [TestMethod]
        public void TestEquality_KilogramToPound_EquivalentValue()
        {
            var kilogram = new QuantityWeight(1.0, WeightUnit.Kilogram);
            var pound = new QuantityWeight(2.2046244201837775, WeightUnit.Pound);

            Assert.IsTrue(kilogram.Equals(pound));
            Assert.IsTrue(pound.Equals(kilogram));
        }

        [TestMethod]
        public void TestEquality_GramToPound_EquivalentValue()
        {
            var gram = new QuantityWeight(453.592, WeightUnit.Gram);
            var pound = new QuantityWeight(1.0, WeightUnit.Pound);

            Assert.IsTrue(gram.Equals(pound));
        }

        [TestMethod]
        public void TestEquality_WeightVsLength_Incompatible()
        {
            object weight = new QuantityWeight(1.0, WeightUnit.Kilogram);
            object length = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsFalse(weight.Equals(length));
        }

        [TestMethod]
        public void TestEquality_NullComparison()
        {
            var weight = new QuantityWeight(1.0, WeightUnit.Kilogram);

            Assert.IsFalse(weight.Equals(null));
        }

        [TestMethod]
        public void TestEquality_SameReference()
        {
            var weight = new QuantityWeight(2.0, WeightUnit.Pound);

            Assert.IsTrue(weight.Equals(weight));
        }

        [TestMethod]
        public void TestEquality_ZeroAcrossUnits()
        {
            var kilogram = new QuantityWeight(0.0, WeightUnit.Kilogram);
            var gram = new QuantityWeight(0.0, WeightUnit.Gram);

            Assert.IsTrue(kilogram.Equals(gram));
        }

        [TestMethod]
        public void TestEquality_NegativeAcrossUnits()
        {
            var kilogram = new QuantityWeight(-1.0, WeightUnit.Kilogram);
            var gram = new QuantityWeight(-1000.0, WeightUnit.Gram);

            Assert.IsTrue(kilogram.Equals(gram));
        }

        [TestMethod]
        public void TestHashCode_EquivalentWeights_AreEqual()
        {
            var kilogram = new QuantityWeight(1.0, WeightUnit.Kilogram);
            var gram = new QuantityWeight(1000.0, WeightUnit.Gram);

            Assert.IsTrue(kilogram.Equals(gram));
            Assert.AreEqual(kilogram.GetHashCode(), gram.GetHashCode());
        }

        [TestMethod]
        public void Constructor_InvalidUnit_ThrowsArgumentException()
        {
            var invalid = (WeightUnit)999;

            Assert.ThrowsException<ArgumentException>(() => new QuantityWeight(1.0, invalid));
        }

        [TestMethod]
        public void Constructor_NaNValue_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new QuantityWeight(double.NaN, WeightUnit.Kilogram)
            );
        }
    }
}
