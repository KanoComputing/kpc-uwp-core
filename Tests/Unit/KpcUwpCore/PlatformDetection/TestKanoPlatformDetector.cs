﻿/**
 * TestKanoPlatformDetector.cs
 *
 * Copyright (c) 2020-2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using KanoComputing.KpcUwpCore.Contracts;
using KanoComputing.KpcUwpCore.PlatformDetection;
using KanoComputing.KpcUwpCore.PlatformDetection.Internal;
using KanoComputing.KpcUwpCore.Tests.Fixtures.KpcUwpCore.PlatformDetection;
using KanoComputing.KpcUwpCore.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;


namespace KanoComputing.KpcUwpCore.Tests.Unit.KpcUwpCore.PlatformDetection {

    [TestClass]
    public class TestKanoPlatformDetector {

        [DataTestMethod]
        [DynamicData(nameof(KanoPlatformIdsFixtures.DeviceSkuIdsWithUnkown),
                     typeof(KanoPlatformIdsFixtures), DynamicDataSourceType.Method)]
        public void TestGetDeviceSku(string productName, string sku, string deviceSku) {
            // Setup any objects and mocks.
            IKEasClientDeviceInformation deviceInfo = new KEasClientDeviceInformation(systemProductName: productName, systemSku: sku);
            IKanoPlatformDetector detector = new KanoPlatformDetector(deviceInfo);

            // Call the method under test.
            string actualSku = detector.GetDeviceSku();

            // Verify the results produced.
            Assert.AreEqual(
                deviceSku, actualSku,
                "Detection function is not returning the expected device SKU");
        }

        [DataTestMethod]
        [DynamicData(nameof(KanoPlatformIdsFixtures.KanoDeviceIdsWithUnkown),
                     typeof(KanoPlatformIdsFixtures), DynamicDataSourceType.Method)]
        public void TestIsKanoDevice(string productName, KanoDevice device) {
            // Setup any objects and mocks.
            IKEasClientDeviceInformation deviceInfo = new KEasClientDeviceInformation(systemProductName: productName);
            IKanoPlatformDetector detector = new KanoPlatformDetector(deviceInfo);
            bool isDeviceListed = KanoPlatformIds.KanoDeviceIds.ContainsKey(productName);

            // Call the method under test.
            bool isKanoDevice = detector.IsKanoDevice();

            // Verify the results produced.
            Assert.AreEqual(
                isDeviceListed, isKanoDevice,
                "The device is listed but the detection function is failing");
            if (isDeviceListed) {
                Assert.IsTrue(
                    device != KanoDevice.Unknown,
                    "A listed device cannot be Unknown");
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(KanoPlatformIdsFixtures.KanoDeviceIdsWithUnkown),
                     typeof(KanoPlatformIdsFixtures), DynamicDataSourceType.Method)]
        public void TestIsKanoPc(string productName, KanoDevice device) {
            // Setup any objects and mocks.
            IKEasClientDeviceInformation deviceInfo = new KEasClientDeviceInformation(systemProductName: productName);
            IKanoPlatformDetector detector = new KanoPlatformDetector(deviceInfo);
            string kpcProductName = KanoPlatformIds.KanoDeviceIds.FirstOrDefault(x => x.Value == KanoDevice.KanoPc).Key;

            // Call the method under test.
            bool isKanoPc = detector.IsKanoPc();

            // Verify the results produced.
            Assert.AreEqual(
                productName == kpcProductName, isKanoPc,
                "The Kano PC identifier is listed but the detection function is failing");
            if (isKanoPc) {
                Assert.IsTrue(
                    device != KanoDevice.Unknown,
                    "A listed device cannot be Unknown");
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(KanoPlatformIdsFixtures.KanoDeviceIdsWithUnkown),
                     typeof(KanoPlatformIdsFixtures), DynamicDataSourceType.Method)]
        public void TestGetKanoDevice(string productName, KanoDevice device) {
            // Setup any objects and mocks.
            IKEasClientDeviceInformation deviceInfo = new KEasClientDeviceInformation(systemProductName: productName);
            IKanoPlatformDetector detector = new KanoPlatformDetector(deviceInfo);

            // Call the method under test.
            KanoDevice actualDevice = detector.GetKanoDevice();

            // Verify the results produced.
            Assert.AreEqual(
                device, actualDevice,
                "Detection function is not returning the expected device");
        }

        [DataTestMethod]
        [DynamicData(nameof(KanoPlatformIdsFixtures.KanoPcSkuIdsWithUnknown),
                     typeof(KanoPlatformIdsFixtures), DynamicDataSourceType.Method)]
        public void TestGetKanoPcSku(string sku, KanoPcSku kpcSku) {
            // Setup any objects and mocks.
            IKEasClientDeviceInformation deviceInfo = new KEasClientDeviceInformation(systemSku: sku);
            IKanoPlatformDetector detector = new KanoPlatformDetector(deviceInfo);

            // Call the method under test.
            KanoPcSku actualSku = detector.GetKanoPcSku();

            // Verify the results produced.
            Assert.AreEqual(
                kpcSku, actualSku,
                "Detection function is not returning the expected Kano PC SKU");
        }
    }
}
