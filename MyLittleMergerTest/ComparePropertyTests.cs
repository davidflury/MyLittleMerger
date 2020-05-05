using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyLittleMerger.Structures.CompareStructure;
using MyLittleMerger.Structures.InstanceStructure;

namespace MyLittleMergerTest
{
    [TestClass]
    public class ComparePropertyTests
    {
        [TestMethod]
        public void TestComparePropertyEqual()
        {
            var result = MergerCompareEngine.Evaluate(new MergerInstanceProperty(null, "Test"), new MergerInstanceProperty(null, "Test"));
            Assert.AreEqual(MergerCompareResult.Equal, result);
        }

        [TestMethod]
        public void TestComparePropertyOnlyLeft()
        {
            var result = MergerCompareEngine.Evaluate(new MergerInstanceProperty(null, "Test"), new MergerInstanceProperty(null, null));
            Assert.AreEqual(MergerCompareResult.OnlyLeft, result);
        }

        [TestMethod]
        public void TestComparePropertyOnlyRight()
        {
            var result = MergerCompareEngine.Evaluate(new MergerInstanceProperty(null, null), new MergerInstanceProperty(null, "Test"));
            Assert.AreEqual(MergerCompareResult.OnlyRight, result);
        }

        [TestMethod]
        public void TestComparePropertyDifferentContent()
        {
            var result = MergerCompareEngine.Evaluate(new MergerInstanceProperty(null, "TestA"), new MergerInstanceProperty(null, "TestB"));
            Assert.AreEqual(MergerCompareResult.Different, result);
        }

        [TestMethod]
        public void TestComparePropertyDifferentType()
        {
            var result = MergerCompareEngine.Evaluate(new MergerInstanceProperty(null, "42"), new MergerInstanceProperty(null, 42));
            Assert.AreEqual(MergerCompareResult.Different, result);
        }
    }
}
