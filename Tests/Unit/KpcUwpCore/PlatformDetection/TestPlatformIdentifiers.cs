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
    }
}
