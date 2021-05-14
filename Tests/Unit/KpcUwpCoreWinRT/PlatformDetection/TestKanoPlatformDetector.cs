/**
 * TestKanoPlatformDetector.cs
 *
 * Copyright (c) 2020-2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using KanoComputing.KpcUwpCore.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace KanoComputing.KpcUwpCore.Tests.Unit.KpcUwpCoreWinRT.PlatformDetection {

    [TestClass]
    public class TestKanoPlatformDetector {

        [TestMethod]
        public void TestGetDeviceSku() {
            KanoComputing.KpcUwpCore.PlatformDetection.IKanoPlatformDetector detector =
                new KanoComputing.KpcUwpCore.PlatformDetection.KanoPlatformDetector();
            KanoComputing.KpcUwpCore.WinRT.PlatformDetection.IKanoPlatformDetector detectorWinRT =
                new KanoComputing.KpcUwpCore.WinRT.PlatformDetection.KanoPlatformDetector();

            string expectedResult = detector.GetDeviceSku();
            string resultWinRT = detectorWinRT.getDeviceSku();

            Assert.AreEqual(
                expectedResult, resultWinRT,
                "WinRT function not wrapping main one correctly");
        }

        [TestMethod]
        public void TestIsKanoPc() {
            KanoComputing.KpcUwpCore.PlatformDetection.IKanoPlatformDetector detector =
                new KanoComputing.KpcUwpCore.PlatformDetection.KanoPlatformDetector();
            KanoComputing.KpcUwpCore.WinRT.PlatformDetection.IKanoPlatformDetector detectorWinRT =
                new KanoComputing.KpcUwpCore.WinRT.PlatformDetection.KanoPlatformDetector();

            bool expectedResult = detector.IsKanoPc();
            bool resultWinRT = detectorWinRT.isKanoPc();

            Assert.AreEqual(
                expectedResult, resultWinRT,
                "WinRT function not wrapping main one correctly");
        }

        [TestMethod]
        public void TestGetKanoPcSku() {
            KanoComputing.KpcUwpCore.PlatformDetection.IKanoPlatformDetector detector =
                new KanoComputing.KpcUwpCore.PlatformDetection.KanoPlatformDetector();
            KanoComputing.KpcUwpCore.WinRT.PlatformDetection.IKanoPlatformDetector detectorWinRT =
                new KanoComputing.KpcUwpCore.WinRT.PlatformDetection.KanoPlatformDetector();

            KanoPcSku expectedSku = detector.GetKanoPcSku();
            KanoPcSku skuWinRT = detectorWinRT.getKanoPcSku();

            Assert.AreEqual(
                expectedSku, skuWinRT,
                "WinRT function not wrapping main one correctly");
        }
    }
}
