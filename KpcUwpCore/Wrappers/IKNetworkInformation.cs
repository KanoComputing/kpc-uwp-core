﻿/**
 * IKNetworkInformation.cs
 *
 * Copyright (c) 2020 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


namespace KanoComputing.KpcUwpCore.Wrappers {

    public interface IKNetworkInformation {

        IKConnectionProfile GetInternetConnectionProfile();
    }
}
