/**
 * TestFeaturePriority.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using KanoComputing.KpcUwpCore.Features;
using KanoComputing.KpcUwpCore.PackageManagement;
using KanoComputing.KpcUwpCore.Tests.Fixtures.KpcUwpCore.Features;
using KanoComputing.KpcUwpCore.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace KanoComputing.KpcUwpCore.Tests.Unit.KpcUwpCore.Features {

    [TestClass]
    public class TestFeaturePriority {

        [DataTestMethod]
        [DynamicData(nameof(FeaturePriorityFixtures.PriorityListWithStatus),
                     typeof(FeaturePriorityFixtures), DynamicDataSourceType.Method)]
        public async Task TestHaveFeaturePriorityAsync(
            IReadOnlyList<Tuple<string, Uri>> priority,
            IReadOnlyDictionary<string, bool> installation,
            string currentApp,
            bool expectedHasPriority) {

            IPackageScanner scanner = Mock.Of<IPackageScanner>();
            Mock.Get(scanner)
                .Setup(x => x.IsAppInstalledAsync(It.IsAny<Uri>(), It.IsAny<string>()))
                .Returns((Uri scheme, string app) => {
                    return Task.FromResult(installation.GetValueOrDefault(app, false));
                });
            IKPackageId packageId = Mock.Of<IKPackageId>();
            Mock.Get(packageId)
                .Setup(x => x.FamilyName)
                .Returns(currentApp);
            IKPackage package = Mock.Of<IKPackage>();
            Mock.Get(package)
                .Setup(x => x.Current)
                .Returns(package);
            Mock.Get(package)
                .Setup(x => x.Id)
                .Returns(packageId);
            IFeaturePriority feature = new FeaturePriority(
                priority, scanner: scanner, package: package);

            bool hasPriority = await feature.HaveFeaturePriorityAsync();

            Assert.AreEqual(
                expectedHasPriority, hasPriority,
                $"Priority mismatch for the current application '{currentApp}'");
        }
    }
}
