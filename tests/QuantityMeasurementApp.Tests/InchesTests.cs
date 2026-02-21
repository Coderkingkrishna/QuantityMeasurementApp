using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Core.Models;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class InchesTests
    {
        [TestMethod]
        public void TestEquality_SameValue()
        {
            var first = new Inches(1.0);
            var second = new Inches(1.0);

            Assert.IsTrue(first.Equals(second));
        }

        [TestMethod]
        public void TestEquality_DifferentValue()
        {
            var first = new Inches(1.0);
            var second = new Inches(2.0);

            Assert.IsFalse(first.Equals(second));
        }

        [TestMethod]
        public void TestEquality_NullComparison()
        {
            var first = new Inches(1.0);

            Assert.IsFalse(first.Equals(null));
        }

        [TestMethod]
        public void TestEquality_SameReference()
        {
            var first = new Inches(1.0);

            Assert.IsTrue(first.Equals(first));
        }

        [TestMethod]
        public void TestEquality_DifferentType()
        {
            var first = new Inches(1.0);

            Assert.IsFalse(first.Equals("1.0"));
        }
    }
}
