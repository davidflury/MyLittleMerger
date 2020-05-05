using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyLittleMerger;
using MyLittleMergerTest.Objects;

namespace MyLittleMergerTest
{
    [TestClass]
    public class FindTests
    {
        [TestMethod]
        public void TestFindByPropertyNodeGender()
        {
            var merger = new MyLittleMerger<ContactObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var genderProperty = typeof(ContactObject).GetProperty(nameof(ContactObject.Gender));

            var success = merger.Structure.TryFind(genderProperty, out var result);

            Assert.IsTrue(success);
            Assert.IsNotNull(result);
            Assert.AreEqual(genderProperty?.Name, result.Identifier);
        }

        [TestMethod]
        public void TestFindByPropertyPropertyCity()
        {
            var merger = new MyLittleMerger<ContactObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var cityProperty = typeof(AddressObject).GetProperty(nameof(AddressObject.City));

            var success = merger.Structure.TryFind(cityProperty, out var result);

            Assert.IsTrue(success);
            Assert.IsNotNull(result);
            Assert.AreEqual(cityProperty?.Name, result.Identifier);
        }

        [TestMethod]
        public void TestFindByPropertyFail()
        {
            var merger = new MyLittleMerger<ContactObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var lonelyProperty = typeof(LittleObject).GetProperty(nameof(LittleObject.LonelyProperty));

            var success = merger.Structure.TryFind(lonelyProperty, out var result);

            Assert.IsFalse(success);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestFindByStringNodeGender()
        {
            var merger = new MyLittleMerger<ContactObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var genderPath = $"{nameof(ContactObject)}.{nameof(ContactObject.Gender)}";

            var success = merger.Structure.TryFind(genderPath, out var result);

            Assert.IsTrue(success);
            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(ContactObject.Gender), result.Identifier);
            }

        [TestMethod]
        public void TestFindByStringPropertyCity()
        {
            var merger = new MyLittleMerger<ContactObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var cityPath = $"{nameof(ContactObject)}.{nameof(ContactObject.Addresses)}.{nameof(AddressObject.City)}";

            var success = merger.Structure.TryFind(cityPath, out var result);

            Assert.IsTrue(success);
            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(AddressObject.City), result.Identifier);
        }

        [TestMethod]
        public void TestFindByStringFail()
        {
            var merger = new MyLittleMerger<ContactObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var foobarPath = $"{nameof(ContactObject)}.{nameof(ContactObject.Addresses)}.Foobar";

            var success = merger.Structure.TryFind(foobarPath, out var result);

            Assert.IsFalse(success);
            Assert.IsNull(result);
        }
    }
}
