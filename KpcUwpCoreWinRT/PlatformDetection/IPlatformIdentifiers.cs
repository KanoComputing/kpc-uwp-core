﻿/**
 * IPlatformIdentifiers.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
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


namespace KanoComputing.KpcUwpCore.WinRT.PlatformDetection {

    public interface IPlatformIdentifiers {

        string getDeviceId();
        string getUserId();
        string getSessionId();
        string getAppSessionId();
    }
}
