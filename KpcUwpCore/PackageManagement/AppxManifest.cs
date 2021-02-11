/**
 * AppxManifest.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using Windows.Storage;


namespace KanoComputing.KpcUwpCore.PackageManagement {

    public class AppxManifest : IXmlConfig {

        public XmlDocument Xml { get; internal set; }
        public XmlNamespaceManager Xmlns { get; internal set; }

        /// <summary>
        /// Load the Package.appxmanifest file to an XML doc. This needs to be included with
        /// the application separately as Content.
        /// </summary>
        /// <param name="path">The application URI path to the Package.appxmanifest file.
        /// For example, 'ms-appx:///Fixtures/MyApp/Package.appxmanifest'.</param>
        /// <param name="defaultXmlns">The prefix for a default (unprefixed) XML namespace.</param
        public async Task LoadAsync(Uri path, string defaultXmlns = "appx") {
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(path);
            string data = await FileIO.ReadTextAsync(file);

            this.Xml = new XmlDocument();
            this.Xml.LoadXml(data);
            this.Xmlns = this.LoadAllXmlNamespaces(this.Xml, defaultXmlns);
        }

        /// <summary>
        /// Load all namespaces found in a given XML document.
        /// </summary>
        /// <param name="xml">The XML document.</param>
        /// <param name="defaultXmlns">The prefix for a default (unprefixed) XML namespace.</param>
        /// <returns>The resulting namespace manager for the XML document.</returns>
        private XmlNamespaceManager LoadAllXmlNamespaces(XmlDocument xml, string defaultXmlns) {
            XmlNamespaceManager xmlns = new XmlNamespaceManager(xml.NameTable);
            XPathNavigator navigator = xml.CreateNavigator();
            IDictionary<string, string> localNamespaces;

            while (navigator.MoveToFollowing(XPathNodeType.Element)) {
                localNamespaces = navigator.GetNamespacesInScope(XmlNamespaceScope.Local);

                foreach (KeyValuePair<string, string> localNamespace in localNamespaces) {
                    string prefix = localNamespace.Key;
                    if (string.IsNullOrEmpty(prefix)) {
                        prefix = defaultXmlns;
                    }
                    xmlns.AddNamespace(prefix, localNamespace.Value);
                }
            }
            return xmlns;
        }
    }
}
