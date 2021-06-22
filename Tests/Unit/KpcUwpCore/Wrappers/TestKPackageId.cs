/**
 * TestKPackageId.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using KanoComputing.KpcUwpCore.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windows.ApplicationModel;


namespace KanoComputing.KpcUwpCore.Tests.Unit.KpcUwpCore.Wrappers {

    [TestClass]
    public class TestKPackageId {

        [TestMethod]
        public void TestFamilyName() {
            Assert.AreEqual(Package.Current.Id.FamilyName, new KPackage().Current.Id.FamilyName);
        }

        [TestMethod]
        public void TestFullName() {
            Assert.AreEqual(Package.Current.Id.FullName, new KPackage().Current.Id.FullName);
        }

        [TestMethod]
        public void TestName() {
            Assert.AreEqual(Package.Current.Id.Name, new KPackage().Current.Id.Name);
        }

        [TestMethod]
        public void TestPublisher() {
            Assert.AreEqual(Package.Current.Id.Publisher, new KPackage().Current.Id.Publisher);
        }

        [TestMethod]
        public void TestPublisherId() {
            Assert.AreEqual(Package.Current.Id.PublisherId, new KPackage().Current.Id.PublisherId);
        }

        [TestMethod]
        public void TestResourceId() {
            Assert.AreEqual(Package.Current.Id.ResourceId, new KPackage().Current.Id.ResourceId);
        }
    }
}
