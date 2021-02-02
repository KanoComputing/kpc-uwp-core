/**
 * TestPackageScanner.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;


namespace KanoComputing.KpcUwpCore.Tests.Unit.KpcUwpCoreWinRT.PackageManagement {

    [TestClass]
    public class TestPackageScanner {

        /// <summary>
        /// Sanity checks, not extensive case/data coverage, system dependent.
        /// </summary>
        [DataTestMethod]
        [DataRow("kano-hub:", "KanoComputing.KanoHub_8bypc573fhhxp")]
        [DataRow("kano-projects:", "KanoComputing.KanoProjects_8bypc573fhhxp")]
        [DataRow("how-computers-work:", "KanoComputing.HowComputersWork_8bypc573fhhxp")]
        [DataRow("kano-overture:", "KanoComputing.OverturePC_8bypc573fhhxp")]
        [DataRow("make-art:", "KanoComputing.MakeArt_8bypc573fhhxp")]
        [DataRow("kano-blocks:", "KanoComputing.KanoBlocks_8bypc573fhhxp")]
        public async Task TestIsAppInstalledAsync(string protocolName, string packageFamilyName) {
            KanoComputing.KpcUwpCore.PackageManagement.IPackageScanner scanner =
                new KanoComputing.KpcUwpCore.PackageManagement.PackageScanner();
            KanoComputing.KpcUwpCore.WinRT.PackageManagement.IPackageScanner scannerWinRT =
                new KanoComputing.KpcUwpCore.WinRT.PackageManagement.PackageScanner();

            bool expected = await scanner.IsAppInstalledAsync(new Uri(protocolName), packageFamilyName);
            bool actual = await scannerWinRT.isAppInstalledAsync(protocolName, packageFamilyName);

            Assert.AreEqual(
                expected, actual,
                "WinRT function not wrapping main one correctly");
        }
    }
}
