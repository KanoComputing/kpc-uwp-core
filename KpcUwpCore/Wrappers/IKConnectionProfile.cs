/**
 * IKConnectionProfile.cs
 *
 * Copyright (c) 2020 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using Windows.Networking.Connectivity;


namespace KanoComputing.KpcUwpCore.Wrappers {

    public interface IKConnectionProfile {

        NetworkConnectivityLevel GetNetworkConnectivityLevel();
    }
}
