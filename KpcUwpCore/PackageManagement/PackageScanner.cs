/**
 * PackageScanner.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.System;


namespace KanoComputing.KpcUwpCore.PackageManagement {

    public class PackageScanner : IPackageScanner {

        public async Task<bool> IsAppInstalledAsync(Uri protocolName, string packageFamilyName) {
            try {
                LaunchQuerySupportStatus statusUri =
                    await Launcher.QueryUriSupportAsync(
                        protocolName, LaunchQuerySupportType.Uri, packageFamilyName);

                LaunchQuerySupportStatus statusResults =
                    await Launcher.QueryUriSupportAsync(
                        protocolName, LaunchQuerySupportType.UriForResults, packageFamilyName);

                bool appInstalled =
                    statusUri == LaunchQuerySupportStatus.Available
                    || statusResults == LaunchQuerySupportStatus.Available;

                Debug.WriteLine("PackageScanner: IsAppInstalledAsync: " +
                    $"Protocol: {protocolName}, PackageFamilyName: {packageFamilyName}, " +
                    $"SupportsUri: {statusUri}, SupportsResults: {statusResults}, " +
                    $"Installed: {appInstalled}");
                return appInstalled;

            } catch (Exception e) {
                Debug.WriteLine($"PackageScanner: IsAppInstalledAsync: Caught: {e}");
                return false;
            }
        }
    }
}
