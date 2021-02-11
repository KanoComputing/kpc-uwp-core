/**
 * AppxManifest.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using System;
using System.Threading.Tasks;
using System.Xml;


namespace KanoComputing.KpcUwpCore.PackageManagement {

    public interface IXmlConfig {

        XmlDocument Xml { get; }
        XmlNamespaceManager Xmlns { get; }

        Task LoadAsync(Uri path, string defaultXmlns = "dflt");
    }
}
