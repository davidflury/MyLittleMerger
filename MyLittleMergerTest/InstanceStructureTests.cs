using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyLittleMerger;
using MyLittleMerger.Structures.InstanceStructure;
using MyLittleMergerTest.Objects;

namespace MyLittleMergerTest
{
    [TestClass]
    public class InstanceStructureTests
    {
        [TestMethod]
        public void TestLittleObjectStructure()
        {
            var merger = new MyLittleMerger<LittleObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var littleObject = new LittleObject { LonelyProperty = "Hello there" };
            var instanceStructure = new MergerInstanceStructure<LittleObject>(merger.Structure, littleObject);

            Assert.AreEqual(typeof(LittleObject), instanceStructure.TypeStructure.Type);
            Assert.IsTrue(instanceStructure.HasValue);
            Assert.AreEqual(0, instanceStructure.Nodes.Count);
            Assert.AreEqual(1, instanceStructure.Properties.Count);
            Assert.AreEqual(1, instanceStructure.Properties.First().Value.Count);
            Assert.AreEqual(typeof(string), instanceStructure.Properties.First().Value.First().TypeProperty.Type);
            Assert.AreEqual(littleObject.LonelyProperty, instanceStructure.Properties.First().Value.First().Value);
        }

        [TestMethod]
        public void TestLittleObjectPrint()
        {
            var merger = new MyLittleMerger<LittleObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var littleObject = new LittleObject() { LonelyProperty = "Hello there" };
            var instanceStructure = new MergerInstanceStructure<LittleObject>(merger.Structure, littleObject);
            var print = instanceStructure.Print();
            
            Assert.IsTrue(print.Contains($"+ LittleObject"));
            Assert.IsTrue(print.Contains($"{nameof(LittleObject.LonelyProperty)} - {nameof(String)}: {littleObject.LonelyProperty}"));
        }

        [TestMethod]
        public void TestContactObjectStructure()
        {
            var merger = new MyLittleMerger<ContactObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var instanceStructure = new MergerInstanceStructure<ContactObject>(merger.Structure, ContactData.David);

            Assert.AreEqual(typeof(ContactObject), instanceStructure.TypeStructure.Type);
            Assert.AreEqual(5, instanceStructure.Nodes.Count);
            Assert.AreEqual(6, instanceStructure.Properties.Count);
            Assert.AreEqual(typeof(Guid), instanceStructure.Properties.First().Value.First().TypeProperty.Type);
            Assert.AreEqual(ContactData.David.Id, instanceStructure.Properties.First().Value.First().Value);
        }

        [TestMethod]
        public void TestContactObjectPrint()
        {
            var merger = new MyLittleMerger<ContactObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var item = ContactData.David;
            var instanceStructure = new MergerInstanceStructure<ContactObject>(merger.Structure, item);
            var print = instanceStructure.Print();
            
            Assert.IsTrue(print.Contains($"{nameof(ContactObject.Id)} - {nameof(Guid)}: {item.Id}"));
        }

        [TestMethod]
        public void TestNullInstance()
        {
            var merger = new MyLittleMerger<LittleObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var instanceStructure = new MergerInstanceStructure<LittleObject>(merger.Structure, null);
            var print = instanceStructure.Print();

            Assert.IsFalse(instanceStructure.HasValue);
            Assert.IsTrue(print.Contains($"+ LittleObject"));
            Assert.IsTrue(print.Contains($"{nameof(LittleObject.LonelyProperty)} - {nameof(String)}: NULL"));
        }

        [TestMethod]
        public void TestPropertyNullString()
        {
            var merger = new MyLittleMerger<LittleObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var item = new LittleObject() { LonelyProperty = null };
            var instanceStructure = new MergerInstanceStructure<LittleObject>(merger.Structure, item);
            var print = instanceStructure.Print();

            Assert.IsNull(instanceStructure.Properties.First().Value.First().Value);
            Assert.IsFalse(instanceStructure.Properties.First().Value.First().HasValue);
            Assert.IsTrue(print.Contains($"{nameof(LittleObject.LonelyProperty)} - {nameof(String)}: NULL"));
        }

        [TestMethod]
        public void TestPropertyNullArray()
        {
            var merger = new MyLittleMerger<NamedArrayObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var item = new NamedArrayObject() { Name = "Test", Items = null };
            var instanceStructure = new MergerInstanceStructure<NamedArrayObject>(merger.Structure, item);
            var print = instanceStructure.Print();

            Assert.IsTrue(print.Contains($"{nameof(NamedArrayObject.Name)} - {nameof(String)}: {item.Name}"));
            Assert.IsTrue(print.Contains($"{nameof(NamedArrayObject.Items)} - {nameof(String)}[]"));
        }

        [TestMethod]
        public void TestNodeNullArray()
        {
            var merger = new MyLittleMerger<ContactObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var item = new ContactObject();
            var instanceStructure = new MergerInstanceStructure<ContactObject>(merger.Structure, item);
            var print = instanceStructure.Print();

            Assert.IsTrue(print.Contains($"{nameof(ContactObject.Addresses)} - {nameof(AddressObject)}[]"));
        }
    }
}
