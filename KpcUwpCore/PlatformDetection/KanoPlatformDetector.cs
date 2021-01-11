/**
 * KanoPlatformDetector.cs
 *
 * Copyright (c) 2020 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using KanoComputing.KpcUwpCore.Contracts;
using KanoComputing.KpcUwpCore.PlatformDetection.Internal;
using KanoComputing.KpcUwpCore.Wrappers;
using System.Diagnostics;


namespace KanoComputing.KpcUwpCore.PlatformDetection {

    public class KanoPlatformDetector : IKanoPlatformDetector {

        private readonly IKEasClientDeviceInformation deviceInfo = null;

        public KanoPlatformDetector(IKEasClientDeviceInformation deviceInfo = null) {
            // TODO: Inject this dependency.
            this.deviceInfo = deviceInfo ?? new KEasClientDeviceInformation();
        }

        public bool IsKanoDevice() {
            return this.GetKanoDevice() != KanoDevice.Unknown;
        }

        public bool IsKanoPc() {
            return this.GetKanoDevice() == KanoDevice.KanoPc;
        }

        public bool IsKanoPcRetail() {
            return
                this.IsKanoPc() &&
                this.GetKanoPcSku() == KanoPcSku.Retail;
        }
        public bool IsKanoPcEducation() {
            return
                this.IsKanoPc() &&
                this.GetKanoPcSku() == KanoPcSku.Education;
        }

        public KanoDevice GetKanoDevice() {
            string model = this.deviceInfo.SystemProductName;
            Debug.WriteLine($"{this.GetType()}: GetKanoDevice: SystemProductName is '{model}'");
            return KanoPlatformIds.GetDeviceById(model);
        }

        public KanoPcSku GetKanoPcSku() {
            string sku = this.deviceInfo.SystemSku;
            Debug.WriteLine($"{this.GetType()}: GetKanoPcSku: SystemSku is '{sku}'");
            return KanoPlatformIds.GetKanoPcSkuById(sku);
        }
    }
}
