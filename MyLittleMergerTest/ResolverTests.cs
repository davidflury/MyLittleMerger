using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLittleMerger;
using MyLittleMerger.Structures.CompareStructure;
using MyLittleMergerTest.Objects;

namespace MyLittleMergerTest
{
    [TestClass]
    public class ResolverTests
    {
        [TestMethod]
        public void TestResolveStringProperty()
        {
            var merger = new MyLittleMerger<ContactObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var executed = false;
            merger.RegisterResolver("ContactObject.FirstName", (left, right) =>
            {
                left.Value += right.Value.ToString();
                executed = true;
                return left;
            });
            var instance = ContactData.David;
            var compare = ContactData.Jennifer;
            merger.Resolve(instance, compare);

            Assert.IsTrue(executed);
            Assert.AreEqual(ContactData.David.FirstName + ContactData.Jennifer.FirstName, instance.FirstName);
        }

        [TestMethod]
        public void TestResolveStringPropertyTyped()
        {
            var merger = new MyLittleMerger<ContactObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var executed = false;
            merger.RegisterResolver<string>("ContactObject.FirstName", (left, right) =>
            {
                left.Value += right.Value;
                executed = true;
                return left;
            });
            var instance = ContactData.David;
            var compare = ContactData.Jennifer;
            merger.Resolve(instance, compare);

            Assert.IsTrue(executed);
            Assert.AreEqual(ContactData.David.FirstName + ContactData.Jennifer.FirstName, instance.FirstName);
            Assert.AreEqual(ContactData.Jennifer.FirstName, compare.FirstName);
        }

        [TestMethod]
        public void TestResolveWrongType()
        {
            var merger = new MyLittleMerger<ContactObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var executed = false;
            merger.RegisterResolver("ContactObject.FirstName", (left, right) =>
            {
                left.Value = 12;
                executed = true;
                return left;
            });
            var instance = ContactData.David;
            var compare = ContactData.Jennifer;
            try
            {
                merger.Resolve(instance, compare);
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(executed);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestResolveStructureNonRun()
        {
            var merger = new MyLittleMerger<ContactObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var executed = false;
            merger.RegisterResolver<SocialMediaObject>("ContactObject.TwitterAccount", (left, right) =>
            {
                left.Value = new SocialMediaObject() { Name = "NEW" };
                executed = true;
                return left;
            });
            var instance = ContactData.David;
            var compare = ContactData.Jennifer;
            merger.Resolve(instance, compare);

            Assert.IsFalse(executed);
            Assert.AreEqual(ContactData.David.TwitterAccount.Name, instance.TwitterAccount.Name);
        }

        [TestMethod]
        public void TestResolveStructureOnlyLeft()
        {
            var merger = new MyLittleMerger<ContactObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var executed = false;
            merger.RegisterResolver<SocialMediaObject>("ContactObject.TwitterAccount", (left, right) =>
            {
                left.Value = new SocialMediaObject() { Name = "NEW" };
                executed = true;
                return left;
            }, new MergerResolvingOptions { RunOnResults = new[] { MergerCompareResult.OnlyLeft } });
            var instance = ContactData.David;
            var compare = ContactData.Jennifer;
            merger.Resolve(instance, compare);

            Assert.IsTrue(executed);
            Assert.AreEqual("NEW", instance.TwitterAccount.Name);
        }

        [TestMethod]
        public void TestResolveStructureOnlyRight()
        {
            var merger = new MyLittleMerger<ContactObject>();
            merger.Initialize(new MergerOptions
            {
                AutoAddTypeAssembly = true
            });
            var executed = false;
            merger.RegisterResolver<SocialMediaObject>("ContactObject.TwitterAccount", (left, right) =>
            {
                left.Value = new SocialMediaObject() { Name = "NEW" };
                executed = true;
                return left;
            }, new MergerResolvingOptions { RunOnResults = new[] { MergerCompareResult.OnlyRight } });
            var instance = ContactData.Jennifer;
            var compare = ContactData.David;
            merger.Resolve(instance, compare);

            Assert.IsTrue(executed);
            Assert.AreEqual("NEW", instance.TwitterAccount.Name);
        }
    }
}