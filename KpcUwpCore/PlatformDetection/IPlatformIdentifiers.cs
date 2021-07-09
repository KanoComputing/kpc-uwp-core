/**
 * IPlatformIdentifiers.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


namespace KanoComputing.KpcUwpCore.PlatformDetection {

    public interface IPlatformIdentifiers {

        string GetDeviceId();
        string GetUserId();
        string GetSessionId();

        string RefreshAppSessionId();
        string GetAppSessionId();
    }
}
