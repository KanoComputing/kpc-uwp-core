/**
 * TestKanoPlatformDetector.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace KanoComputing.KpcUwpCore.Tests.Unit.KpcUwpCoreWinRT.PlatformDetection {

    [TestClass]
    public class TestPlatformIdentifiers {

        [TestMethod]
        public void TestGetDeviceId() {
            KanoComputing.KpcUwpCore.PlatformDetection.IPlatformIdentifiers ids =
                new KanoComputing.KpcUwpCore.PlatformDetection.PlatformIdentifiers();
            KanoComputing.KpcUwpCore.WinRT.PlatformDetection.IPlatformIdentifiers idsWinRT =
                new KanoComputing.KpcUwpCore.WinRT.PlatformDetection.PlatformIdentifiers();

            string expectedResult = ids.GetDeviceId();
            string resultWinRT = idsWinRT.getDeviceId();

            Assert.AreEqual(
                expectedResult, resultWinRT,
                "WinRT function not wrapping main one correctly");
        }

        [TestMethod]
        public void TestUserId() {
            KanoComputing.KpcUwpCore.PlatformDetection.IPlatformIdentifiers ids =
                new KanoComputing.KpcUwpCore.PlatformDetection.PlatformIdentifiers();
            KanoComputing.KpcUwpCore.WinRT.PlatformDetection.IPlatformIdentifiers idsWinRT =
                new KanoComputing.KpcUwpCore.WinRT.PlatformDetection.PlatformIdentifiers();

            string expectedResult = ids.GetUserId();
            string resultWinRT = idsWinRT.getUserId();

            Assert.AreEqual(
                expectedResult, resultWinRT,
                "WinRT function not wrapping main one correctly");
        }

        [TestMethod]
        public void TestSessionId() {
            KanoComputing.KpcUwpCore.PlatformDetection.IPlatformIdentifiers ids =
                new KanoComputing.KpcUwpCore.PlatformDetection.PlatformIdentifiers();
            KanoComputing.KpcUwpCore.WinRT.PlatformDetection.IPlatformIdentifiers idsWinRT =
                new KanoComputing.KpcUwpCore.WinRT.PlatformDetection.PlatformIdentifiers();

            string expectedResult = ids.GetSessionId();
            string resultWinRT = idsWinRT.getSessionId();

            Assert.AreEqual(
                expectedResult, resultWinRT,
                "WinRT function not wrapping main one correctly");
        }

        [TestMethod]
        public void TestAppSessionId() {
            KanoComputing.KpcUwpCore.PlatformDetection.IPlatformIdentifiers ids =
                new KanoComputing.KpcUwpCore.PlatformDetection.PlatformIdentifiers();
            KanoComputing.KpcUwpCore.WinRT.PlatformDetection.IPlatformIdentifiers idsWinRT =
                new KanoComputing.KpcUwpCore.WinRT.PlatformDetection.PlatformIdentifiers();

            string expectedResult = ids.GetAppSessionId();
            string resultWinRT = idsWinRT.getAppSessionId();

            Assert.AreEqual(
                expectedResult, resultWinRT,
                "WinRT function not wrapping main one correctly");
        }
    }
}
