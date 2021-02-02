/**
 * PackageScanner.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 *
 * Windows Runtime Component wrapper.
 *
 * NOTE:
 *     Method signatures have been renamed to use camel case due to the fact
 *     that they get implicitly renamed without warning when objects are
 *     exposed to and used from JavaScript. See:
 *     https://docs.microsoft.com/en-us/windows/uwp/winrt-components/walkthrough-creating-a-simple-windows-runtime-component-and-calling-it-from-javascript#call-the-component-from-javascript
 */


using System;
using Windows.Foundation;
using Windows.Foundation.Metadata;


namespace KanoComputing.KpcUwpCore.WinRT.PackageManagement {

    [AllowForWeb]
    public sealed class PackageScanner : IPackageScanner {

        private readonly KanoComputing.KpcUwpCore.PackageManagement.PackageScanner packageScanner;

        public PackageScanner() {
            this.packageScanner = new KanoComputing.KpcUwpCore.PackageManagement.PackageScanner();
        }

        public IAsyncOperation<bool> isAppInstalledAsync(string protocolName, string packageFamilyName) {
            return this.packageScanner.IsAppInstalledAsync(new Uri(protocolName), packageFamilyName).AsAsyncOperation();
        }
    }
}
