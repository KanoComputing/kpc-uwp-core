/**
 * AppUpdater.cs
 *
 * Copyright (c) 2020-2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using KanoComputing.KpcUwpCore.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Services.Store;
using Windows.Storage;


namespace KanoComputing.KpcUwpCore.AppUpdate {

    public class AppUpdater : IAppUpdater {

        private const string FLAG_SET = "1";
        private const string FLAG_UNSET = "0";

        private readonly INetworkStatus network = null;
        private readonly StoreContext storeContext = null;
        private readonly ApplicationDataContainer localSettings = null;

        public AppUpdater(INetworkStatus network = null) {
            this.network = network ?? new NetworkStatus();

            this.storeContext = StoreContext.GetDefault();
            this.localSettings = ApplicationData.Current.LocalSettings;
        }

        /// <summary>
        /// Check the Microsoft Store if there are update available for the app.
        /// </summary>
        public async Task<bool> IsUpdateAvailableAsync() {
            if (!this.network.IsInternetAvailable())
                return false;

            // Get the list of packages for the current app for which there
            // are updates available.
            IReadOnlyList<StorePackageUpdate> updatablePackages =
                await this.storeContext.GetAppAndOptionalStorePackageUpdatesAsync();

            bool result = (updatablePackages.Count > 0);
            Debug.WriteLine($"{this.GetType()}: IsUpdateAvailableAsync: {result}");
            return result;
        }

        /// <summary>
        /// Check the Microsoft Store if there are mandatory updates available
        /// for the app and optionally set a feature flag in local settings.
        /// </summary>
        /// <param name="setFlag">(optional) Whether or not to set the feature flag.</param>
        public async Task<bool> IsMandatoryUpdateAvailableAsync(bool setFlag = true) {
            if (!this.network.IsInternetAvailable())
                return false;

            // Get the list of packages for the current app for which there
            // are updates available.
            IReadOnlyList<StorePackageUpdate> updatablePackages =
                await this.storeContext.GetAppAndOptionalStorePackageUpdatesAsync();

            bool result = (
                updatablePackages.Count > 0 &&
                updatablePackages.Any(u => u.Mandatory)
            );
            Debug.WriteLine($"{this.GetType()}: IsMandatoryUpdateAvailableAsync: {result}");

            // Set the mandatory updates feature flag.
            if (setFlag) {
                this.SetMandatoryUpdateAvailableFlag(result);
            }
            return result;
        }

        private void SetMandatoryUpdateAvailableFlag(bool value) {
            this.localSettings.Values["MANDATORY_UPDATES_AVAILABLE"] = value ? FLAG_SET : FLAG_UNSET;
        }

        /// <summary>
        /// Check the result of the last execution of IsMandatoryUpdateAvailableAsync()
        /// via the feature flag in local settings.
        /// </summary>
        /// <remarks>
        /// To verify that IsMandatoryUpdateAvailableAsync() had computed the value
        /// of the flag before, use IsMandatoryUpdateFlagComputed().
        /// </remarks>
        public bool IsMandatoryUpdateAvailableViaFlag() {
            bool result = (
                this.IsMandatoryUpdateFlagComputed() &&
                this.localSettings.Values["MANDATORY_UPDATES_AVAILABLE"].ToString() == FLAG_SET
            );
            Debug.WriteLine($"{this.GetType()}: IsMandatoryUpdateAvailableViaFlag: {result}");
            return result;
        }

        /// <summary>
        /// Check if the mandatory updates flag was computed and stored in local settings
        /// from a previous call to IsMandatoryUpdateAvailableAsync().
        /// </summary>
        public bool IsMandatoryUpdateFlagComputed() {
            bool result = this.localSettings.Values.ContainsKey("MANDATORY_UPDATES_AVAILABLE");
            Debug.WriteLine($"{this.GetType()}: IsMandatoryUpdateAvailableFlagSet: {result}");
            return result;
        }
    }
}
