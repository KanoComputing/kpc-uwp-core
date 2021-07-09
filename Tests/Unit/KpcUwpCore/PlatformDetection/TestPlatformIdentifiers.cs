/**
 * TestPlatformIdentifiers.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using KanoComputing.KpcUwpCore.PlatformDetection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using Windows.Storage;


namespace KanoComputing.KpcUwpCore.Tests.Unit.KpcUwpCore.PlatformDetection {

    [TestClass]
    public class TestPlatformIdentifiers {

        [TestMethod]
        public void TestGetDeviceId() {
            IPlatformIdentifiers platform = new PlatformIdentifiers();
            Random random = new Random();

            string id = platform.GetDeviceId();

            Assert.IsFalse(
                string.IsNullOrEmpty(id));

            for (int i = 0; i < 10; i++) {
                Thread.Sleep(random.Next(10, 100));
                Assert.AreEqual(
                    id, platform.GetDeviceId());
            }
        }

        [TestMethod]
        public void TestUserId() {
            IPlatformIdentifiers platform = new PlatformIdentifiers();
            Random random = new Random();

            string id = platform.GetUserId();

            Assert.IsFalse(
                string.IsNullOrEmpty(id));

            for (int i = 0; i < 10; i++) {
                Thread.Sleep(random.Next(10, 100));
                Assert.AreEqual(
                    id, platform.GetUserId());
            }
        }

        [TestMethod]
        public void TestSessionId() {
            IPlatformIdentifiers platform = new PlatformIdentifiers();
            Random random = new Random();

            string id = platform.GetSessionId();

            Assert.IsFalse(
                string.IsNullOrEmpty(id));

            for (int i = 0; i < 10; i++) {
                Thread.Sleep(random.Next(10, 100));
                Assert.AreEqual(
                    id, platform.GetSessionId());
            }
        }

        [TestMethod]
        public void TestRefreshAppSessionId() {
            IPlatformIdentifiers platform = new PlatformIdentifiers();
            Random random = new Random();

            for (int i = 0; i < 100; i++) {
                Thread.Sleep(random.Next(10, 100));
                string oldSessionId = platform.GetAppSessionId();
                string newSessionId = platform.RefreshAppSessionId();

                Assert.AreNotEqual(
                    oldSessionId, newSessionId,
                    "The session ID never changed");
                Assert.AreEqual(
                    newSessionId, platform.GetAppSessionId(),
                    "The session ID returned when refreshing differs from that which was saved");
            }
        }

        [TestMethod]
        public void TestRefreshAppSessionIdNeverGenerated() {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey(PlatformIdentifiers.APP_SESSION_KEY)) {
                localSettings.Values.Remove(PlatformIdentifiers.APP_SESSION_KEY);
            }

            string sessionId = new PlatformIdentifiers().RefreshAppSessionId();

            Assert.AreNotEqual(
                string.Empty, sessionId,
                "Method should never return an empty string");
        }

        [TestMethod]
        public void TestRefreshAppSessionIdPreviouslyGenerated() {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            Random rand = new Random();
            string originalValue = rand.Next().ToString();

            if (!localSettings.Values.ContainsKey(PlatformIdentifiers.APP_SESSION_KEY)) {
                localSettings.Values.Add(PlatformIdentifiers.APP_SESSION_KEY, originalValue);
            }

            string sessionId = new PlatformIdentifiers().RefreshAppSessionId();

            Assert.AreNotEqual(
                string.Empty, sessionId,
                "Method should never return an empty string");
            Assert.AreNotEqual(
                originalValue, sessionId,
                "RefreshSessionId() returned old value");
        }

        [TestMethod]
        public void TestGetAppSessionIdNeverGenerated() {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey(PlatformIdentifiers.APP_SESSION_KEY)) {
                localSettings.Values.Remove(PlatformIdentifiers.APP_SESSION_KEY);
            }

            string sessionId = new PlatformIdentifiers().GetAppSessionId();

            Assert.AreNotEqual(
                string.Empty, sessionId,
                "App session ID was not generated for the first time prior to returning");
        }

        [TestMethod]
        public void TestGetAppSessionIdPreviouslyGenerated() {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            Random rand = new Random();
            string originalValue = rand.Next().ToString();

            if (!localSettings.Values.ContainsKey(PlatformIdentifiers.APP_SESSION_KEY)) {
                localSettings.Values.Add(PlatformIdentifiers.APP_SESSION_KEY, originalValue);
            }

            string sessionId = new PlatformIdentifiers().GetAppSessionId();

            Assert.AreNotEqual(
                string.Empty, sessionId,
                "Method should never return an empty string");
            Assert.AreNotEqual(
                originalValue, sessionId,
                "GetSessionId() returned old value");
        }
    }
}
