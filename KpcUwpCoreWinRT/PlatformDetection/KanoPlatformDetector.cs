/**
 * KanoPlatformDetector.cs
 *
 * Copyright (c) 2020 Kano Computing Ltd.
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


using KanoComputing.KpcUwpCore.Contracts;
using Windows.Foundation.Metadata;


namespace KanoComputing.KpcUwpCore.WinRT.PlatformDetection {

    [AllowForWeb]
    public sealed class KanoPlatformDetector : IKanoPlatformDetector {

        private readonly KanoComputing.KpcUwpCore.PlatformDetection.IKanoPlatformDetector kanoPlatformDetector;

        public KanoPlatformDetector() {
            this.kanoPlatformDetector = new KanoComputing.KpcUwpCore.PlatformDetection.KanoPlatformDetector();
        }

        public bool isKanoPc() {
            return this.kanoPlatformDetector.IsKanoPc();
        }

        public KanoPcSku getKanoPcSku() {
            return this.kanoPlatformDetector.GetKanoPcSku();
        }
    }
}
