using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyLittleMerger;
using MyLittleMerger.Helpers;
using MyLittleMerger.Structures.CompareStructure;
using MyLittleMerger.Structures.InstanceStructure;
using MyLittleMergerTest.Objects;

namespace MyLittleMergerTest
{
    [TestClass]
    public class CompareStructureTests
    {
        [TestMethod]
        public void TestCompareStructurePropertyEqualString()
        {
            var merger = new MyLittleMerger<NamedArrayObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });

            var itemA = new NamedArrayObject { Name = "Item" };
            var instanceStructureA = new MergerInstanceStructure<NamedArrayObject>(merger.Structure, itemA);
            var itemB = new NamedArrayObject { Name = "Item" };
            var instanceStructureB = new MergerInstanceStructure<NamedArrayObject>(merger.Structure, itemB);

            var mergeStructure = new MergerCompareStructure<NamedArrayObject>(instanceStructureA, instanceStructureB);
            Assert.AreEqual(itemA, mergeStructure.Instance.Value);
            Assert.AreEqual(itemB, mergeStructure.Compare.Value);

            Assert.AreEqual(0, mergeStructure.Nodes.Count);
            Assert.AreEqual(2, mergeStructure.Properties.Count);

            Assert.AreEqual(MergerCompareResult.Equal, mergeStructure.Properties.First().Value.First().Result);
            Assert.AreEqual(MergerCompareResult.Equal, mergeStructure.Result);
        }

        [TestMethod]
        public void TestCompareStructurePropertyOnlyLeftString()
        {
            var merger = new MyLittleMerger<NamedArrayObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });

            var itemA = new NamedArrayObject { Name = "ItemA" };
            var instanceStructureA = new MergerInstanceStructure<NamedArrayObject>(merger.Structure, itemA);
            var instanceStructureB = new MergerInstanceStructure<NamedArrayObject>(merger.Structure, null);
            var mergeStructure = new MergerCompareStructure<NamedArrayObject>(instanceStructureA, instanceStructureB);

            Assert.AreEqual(MergerCompareResult.OnlyLeft, mergeStructure.Properties.First().Value.First().Result);
            Assert.AreEqual(MergerCompareResult.OnlyLeft, mergeStructure.Result);
        }

        [TestMethod]
        public void TestCompareStructurePropertyOnlyRightString()
        {
            var merger = new MyLittleMerger<NamedArrayObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });

            var instanceStructureA = new MergerInstanceStructure<NamedArrayObject>(merger.Structure, null);
            var itemB = new NamedArrayObject { Name = "ItemB" };
            var instanceStructureB = new MergerInstanceStructure<NamedArrayObject>(merger.Structure, itemB);
            var mergeStructure = new MergerCompareStructure<NamedArrayObject>(instanceStructureA, instanceStructureB);

            Assert.AreEqual(MergerCompareResult.OnlyRight, mergeStructure.Properties.First().Value.First().Result);
            Assert.AreEqual(MergerCompareResult.OnlyRight, mergeStructure.Result);
        }

        [TestMethod]
        public void TestCompareStructurePropertyDifferentString()
        {
            var merger = new MyLittleMerger<NamedArrayObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });

            var itemA = new NamedArrayObject { Name = "ItemA" };
            var instanceStructureA = new MergerInstanceStructure<NamedArrayObject>(merger.Structure, itemA);
            var itemB = new NamedArrayObject { Name = "ItemB" };
            var instanceStructureB = new MergerInstanceStructure<NamedArrayObject>(merger.Structure, itemB);
            var mergeStructure = new MergerCompareStructure<NamedArrayObject>(instanceStructureA, instanceStructureB);

            Assert.AreEqual(MergerCompareResult.Different, mergeStructure.Properties.First().Value.First().Result);
            Assert.AreEqual(MergerCompareResult.Different, mergeStructure.Result);
        }

        [TestMethod]
        public void TestCompareStructurePropertyOnlyLeftProperty()
        {
            var merger = new MyLittleMerger<NamedArrayObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });

            var itemA = new NamedArrayObject { Name = "ItemA", Items = new[] { "A1", "C", "A3" } };
            var instanceStructureA = new MergerInstanceStructure<NamedArrayObject>(merger.Structure, itemA);
            var itemB = new NamedArrayObject { Name = null, Items = new[] { "B1", "C", "B3" } };
            var instanceStructureB = new MergerInstanceStructure<NamedArrayObject>(merger.Structure, itemB);
            var mergeStructure = new MergerCompareStructure<NamedArrayObject>(instanceStructureA, instanceStructureB);

            Assert.AreEqual(MergerCompareResult.OnlyLeft, mergeStructure.Properties.First().Value.First().Result);
            Assert.AreEqual(MergerCompareResult.Different, mergeStructure.Properties.Second().Value.First().Result);
            Assert.AreEqual(MergerCompareResult.Equal, mergeStructure.Properties.Second().Value.Second().Result);
            Assert.AreEqual(MergerCompareResult.Different, mergeStructure.Result);
        }

        [TestMethod]
        public void TestCompareStructurePropertyOnlyRightProperty()
        {
            var merger = new MyLittleMerger<NamedArrayObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });

            var itemA = new NamedArrayObject { Name = null, Items = new[] { "A1", "C", "A3" } };
            var instanceStructureA = new MergerInstanceStructure<NamedArrayObject>(merger.Structure, itemA);
            var itemB = new NamedArrayObject { Name = "ItemB", Items = new[] { "B1", "C", "B3" } };
            var instanceStructureB = new MergerInstanceStructure<NamedArrayObject>(merger.Structure, itemB);
            var mergeStructure = new MergerCompareStructure<NamedArrayObject>(instanceStructureA, instanceStructureB);

            Assert.AreEqual(MergerCompareResult.OnlyRight, mergeStructure.Properties.First().Value.First().Result);
            Assert.AreEqual(MergerCompareResult.Different, mergeStructure.Properties.Second().Value.First().Result);
            Assert.AreEqual(MergerCompareResult.Equal, mergeStructure.Properties.Second().Value.Second().Result);
            Assert.AreEqual(MergerCompareResult.Different, mergeStructure.Result);
        }

        [TestMethod]
        public void TestCompareStructurePropertyArray()
        {
            var merger = new MyLittleMerger<NamedArrayObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });

            var itemA = new NamedArrayObject { Name = null, Items = new[] { "A", null, "B", "C" } };
            var instanceStructureA = new MergerInstanceStructure<NamedArrayObject>(merger.Structure, itemA);
            var itemB = new NamedArrayObject { Name = null, Items = new[] { "A", "B", "C" } };
            var instanceStructureB = new MergerInstanceStructure<NamedArrayObject>(merger.Structure, itemB);
            var mergeStructure = new MergerCompareStructure<NamedArrayObject>(instanceStructureA, instanceStructureB);

            var itemsProperties = mergeStructure.Properties.First(p => Equals(p.Key.Identifier, nameof(NamedArrayObject.Items))).Value;

            Assert.AreEqual(MergerCompareResult.Equal, itemsProperties[0].Result);
            Assert.AreEqual(MergerCompareResult.OnlyRight, itemsProperties[1].Result);
            Assert.AreEqual(MergerCompareResult.Different, itemsProperties[2].Result);
            Assert.AreEqual(MergerCompareResult.OnlyLeft, itemsProperties[3].Result);
            Assert.AreEqual(MergerCompareResult.Different, mergeStructure.Result);
        }

        [TestMethod]
        public void TestCompareStructurePropertyPrint()
        {
            var merger = new MyLittleMerger<NamedArrayObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });

            var itemA = new NamedArrayObject { Name = "ItemA", Items = new[] { "A", null, "B", "C" } };
            var instanceStructureA = new MergerInstanceStructure<NamedArrayObject>(merger.Structure, itemA);
            var itemB = new NamedArrayObject { Name = "ItemB", Items = new[] { "A", "B", "C" } };
            var instanceStructureB = new MergerInstanceStructure<NamedArrayObject>(merger.Structure, itemB);
            var mergeStructure = new MergerCompareStructure<NamedArrayObject>(instanceStructureA, instanceStructureB);
            var print = mergeStructure.Print();

            Assert.IsTrue(print.Contains($"{nameof(NamedArrayObject)}: {MergerCompareResult.Different}"));
            Assert.IsTrue(print.Contains($"A <> A: {MergerCompareResult.Equal}"));
            Assert.IsTrue(print.Contains($"NULL <> B: {MergerCompareResult.OnlyRight}"));
            Assert.IsTrue(print.Contains($"B <> C: {MergerCompareResult.Different}"));
            Assert.IsTrue(print.Contains($"C <> NULL: {MergerCompareResult.OnlyLeft}"));
        }

        [TestMethod]
        public void TestCompareStructureNode()
        {
            var merger = new MyLittleMerger<ContactObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });

            var instanceStructureA = new MergerInstanceStructure<ContactObject>(merger.Structure, ContactData.David);
            var instanceStructureB = new MergerInstanceStructure<ContactObject>(merger.Structure, ContactData.Jennifer);
            var mergeStructure = new MergerCompareStructure<ContactObject>(instanceStructureA, instanceStructureB);
            var print = mergeStructure.Print();

            Assert.IsTrue(print.Contains($"{ContactData.David.FirstName} <> {ContactData.Jennifer.FirstName}: {MergerCompareResult.Different}"));
        }
    }
}
