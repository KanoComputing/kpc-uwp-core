﻿/**
 * IAppUpdater.cs
 *
 * Copyright (c) 2020-2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using System.Threading.Tasks;


namespace KanoComputing.KpcUwpCore.AppUpdate {

    public interface IAppUpdater {

        Task<bool> IsUpdateAvailableAsync();
        Task<bool> IsMandatoryUpdateAvailableAsync(bool setFlag = true);
        bool IsMandatoryUpdateAvailableViaFlag();
        bool IsMandatoryUpdateFlagComputed();
    }
}
