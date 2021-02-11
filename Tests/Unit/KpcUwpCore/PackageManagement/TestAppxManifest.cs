/**
 * TestAppxManifest.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using KanoComputing.KpcUwpCore.PackageManagement;
using KanoComputing.KpcUwpCore.Tests.Fixtures.KpcUwpCore.PackageManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;


namespace KanoComputing.KpcUwpCore.Tests.Unit.KpcUwpCore.PackageManagement {

    [TestClass]
    public class TestAppxManifest {

        [TestMethod]
        public async Task TestLoadAsync() {
            AppxManifest manifest = null;
            try {
                manifest = await AppxManifestFixture.GetAppxManifestAsync();
            } catch (Exception e) {
                Assert.Fail($"Caught: {e}");
            }
            Assert.IsNotNull(
                manifest,
                "There was an error loading the manifest file.");
            Assert.IsNotNull(
                manifest.Xml,
                "The manifest XML document was not loaded properly.");
            Assert.IsNotNull(
                manifest.Xmlns,
                "The manifest XML document namespaces were not loaded properly.");
        }

        [TestMethod]
        public async Task TestXmlns() {
            AppxManifest manifest = await AppxManifestFixture.GetAppxManifestAsync();
            IDictionary<string, string> actualNamespaces =
                manifest.Xmlns.GetNamespacesInScope(XmlNamespaceScope.All);

            CollectionAssert.AreEqual(
               AppxManifestFixture.Namespaces.OrderBy(kv => kv.Key).ToList(),
               actualNamespaces.OrderBy(kv => kv.Key).ToList(),
               "The class did not load all expected namespaces."
            );
        }

        [DataTestMethod]
        [DataRow("/appx:Package/appx:Applications/appx:Application[@Id='App']/appx:Extensions/uap:Extension[@Category='windows.protocol']/uap:Protocol")]
        [DataRow("/appx:Package/appx:Resources/appx:Resource[@Language='EN-US']")]
        [DataRow("/appx:Package/appx:Applications/appx:Application[@Id='App']/appx:Extensions/appx:Extension[@Category='windows.preInstalledConfigTask']")]
        [DataRow("/appx:Package/appx:Capabilities/rescap:Capability")]
        [DataRow("/appx:Package/appx:Applications/appx:Application[@Id='App']/uap:ApplicationContentUriRules/uap:Rule")]
        public async Task TestXmlQueries(string xpath) {
            AppxManifest manifest = await AppxManifestFixture.GetAppxManifestAsync();
            XmlNode node = manifest.Xml.DocumentElement.SelectSingleNode(xpath, manifest.Xmlns);

            Assert.IsNotNull(
                node,
                "Could select a node from the manifest. Is it loaded properly?");
        }
    }
}
