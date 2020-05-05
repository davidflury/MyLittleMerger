using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyLittleMerger;
using MyLittleMerger.Structures.TypeStructure;
using MyLittleMergerTest.Objects;

namespace MyLittleMergerTest
{
    [TestClass]
    public class TypeStructureTests
    {
        [TestMethod]
        public void TestInitialize()
        {
            var merger = new MyLittleMerger<LittleObject>();
            Assert.IsFalse(merger.Initialized);
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });

            Assert.IsTrue(merger.Initialized);
        }

        [TestMethod]
        public void TestLittleObjectStructure()
        {
            var merger = new MyLittleMerger<LittleObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });

            Assert.AreEqual(typeof(LittleObject), merger.Structure.Type);
            Assert.AreEqual(0, merger.Structure.Nodes.Count);
            Assert.AreEqual(1, merger.Structure.Properties.Count);
            Assert.AreEqual(typeof(string), merger.Structure.Properties[0].Type);
        }

        [TestMethod]
        public void TestLittleObjectPrint()
        {
            var merger = new MyLittleMerger<LittleObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var print = merger.Structure.Print();

            Assert.IsFalse(string.IsNullOrEmpty(print));
            Assert.IsTrue(print.Contains($"- {nameof(LittleObject)}"));
            Assert.IsTrue(print.Contains($"{nameof(LittleObject.LonelyProperty)} - String"));
        }

        [TestMethod]
        public void TestContactObjectStructure()
        {
            var merger = new MyLittleMerger<ContactObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });

            Assert.AreEqual(typeof(ContactObject), merger.Structure.Type);
            Assert.AreEqual(5, merger.Structure.Nodes.Count);
            Assert.AreEqual(6, merger.Structure.Properties.Count);
        }

        [TestMethod]
        public void TestContactObjectPrint()
        {
            var merger = new MyLittleMerger<ContactObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var print = merger.Structure.Print();

            Assert.IsFalse(string.IsNullOrEmpty(print));
            Assert.IsTrue(print.Contains($"- {nameof(ContactObject)}"));
            Assert.IsTrue(print.Contains($"{nameof(ContactObject.Id)} - {nameof(Guid)}"));
            Assert.IsTrue(print.Contains($"{nameof(ContactObject.FirstName)} - {nameof(String)}"));
            Assert.IsTrue(print.Contains($"{nameof(ContactObject.Friends)} - {nameof(String)}[]"));
            Assert.IsTrue(print.Contains($"{nameof(ContactObject.Birthday)} - {nameof(DateTime)}?"));
            Assert.IsTrue(print.Contains($"+ {nameof(ContactObject.Gender)} - {nameof(GenderEnum)}?"));
            Assert.IsTrue(print.Contains($"+ {nameof(ContactObject.Addresses)} - {nameof(AddressObject)}[]"));
            Assert.IsTrue(print.Contains($"+ {nameof(AddressObject.County)} - {nameof(CountryObject)}"));
        }
        
        [TestMethod]
        public void TestContactObjectStructureEquality()
        {
            var merger = new MyLittleMerger<ContactObject>(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var structureA = new MergerTypeStructure<ContactObject>(merger);
            var structureB = new MergerTypeStructure<ContactObject>(merger);

            var genderNodeA = structureA.Nodes.First(n => Equals(n.Identifier, nameof(ContactObject.Gender)));
            var genderNodeB = structureB.Nodes.First(n => Equals(n.Identifier, nameof(ContactObject.Gender)));
            // ReSharper disable once PossibleUnintendedReferenceComparison
            Assert.IsFalse(structureA == structureB);
            Assert.AreEqual(genderNodeA, genderNodeB);
            Assert.AreEqual(structureA, structureB);
        }

        [TestMethod]
        public void TestContactObjectLonelyObjectStructureEquality()
        {
            var mergerA = new MyLittleMerger<ContactObject>(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var structureA = new MergerTypeStructure<ContactObject>(mergerA);
            var mergerB = new MyLittleMerger<LittleObject>(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var structureB = new MergerTypeStructure<LittleObject>(mergerB);
            // ReSharper disable once PossibleUnintendedReferenceComparison
            Assert.AreNotEqual(structureA, structureB);
        }

        [TestMethod]
        public void TestLonelyObjectSmallObjectStructureEquality()
        {
            var mergerA = new MyLittleMerger<SmallObject>(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var structureA = new MergerTypeStructure<SmallObject>(mergerA);
            var mergerB = new MyLittleMerger<LittleObject>(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var structureB = new MergerTypeStructure<LittleObject>(mergerB);
            // ReSharper disable once PossibleUnintendedReferenceComparison
            Assert.AreNotEqual(structureA, structureB);
            Assert.AreEqual(structureA.Properties[0], structureB.Properties[0]);
        }
    }
}
