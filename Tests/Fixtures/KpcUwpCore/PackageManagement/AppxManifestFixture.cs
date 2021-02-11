/**
 * AppxManifestFixture.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using KanoComputing.KpcUwpCore.PackageManagement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace KanoComputing.KpcUwpCore.Tests.Fixtures.KpcUwpCore.PackageManagement {

    public class AppxManifestFixture {

        public static readonly Uri AppxManifestPath =
            new Uri("ms-appx:///Fixtures/KpcUwpCore/PackageManagement/Package.appxmanifest");

        public static readonly IDictionary<string, string> Namespaces =
            new Dictionary<string, string> {
                { "xml", "http://www.w3.org/XML/1998/namespace" },
                { "appx", "http://schemas.microsoft.com/appx/manifest/foundation/windows10" },
                { "mp", "http://schemas.microsoft.com/appx/2014/phone/manifest" },
                { "uap", "http://schemas.microsoft.com/appx/manifest/uap/windows10" },
                { "uap3", "http://schemas.microsoft.com/appx/manifest/uap/windows10/3" },
                { "uap4", "http://schemas.microsoft.com/appx/manifest/uap/windows10/4" },
                { "desktop4", "http://schemas.microsoft.com/appx/manifest/desktop/windows10/4" },
                { "iot2", "http://schemas.microsoft.com/appx/manifest/iot/windows10/2" },
                { "rescap", "http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities" }
            };

        public static async Task<AppxManifest> GetAppxManifestAsync() {
            AppxManifest manifest = new AppxManifest();
            await manifest.LoadAsync(AppxManifestPath);
            return manifest;
        }
    }
}
