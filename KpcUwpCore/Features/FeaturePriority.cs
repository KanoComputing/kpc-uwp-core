/**
 * FeaturePriority.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using KanoComputing.KpcUwpCore.PackageManagement;
using KanoComputing.KpcUwpCore.Wrappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace KanoComputing.KpcUwpCore.Features {

    /// <summary>
    /// Given a list of applications that contain an abstract feature,
    /// determine whether the current application should implement that
    /// feature while the others should not.
    ///
    /// For example, suppose a priority list [App1, App2, App3, App4] and
    /// the feature to show a "Welcome" system notification. The method
    /// HaveFeaturePriority() will look for the first installed application
    /// and determine if that is the currently running one. Suppose when the
    /// call is made from within App2 and App1 is not installed, then App2
    /// should be the one showing the notification with App3 and App4 delegating
    /// that responsibility to App2.
    /// </summary>
    public class FeaturePriority : IFeaturePriority {

        private readonly IReadOnlyList<Tuple<string, Uri>> priorityList;
        private readonly IPackageScanner scanner;
        private readonly IKPackage package;

        /// <param name="priorityList">An ordered sequence of application package
        /// family name and URI scheme protocol pairs.</param>
        public FeaturePriority(
            IReadOnlyList<Tuple<string, Uri>> priorityList,
            IPackageScanner scanner = null,
            IKPackage package = null) {

            this.priorityList = priorityList;
            this.scanner = scanner ?? new PackageScanner();
            this.package = package ?? new KPackage();
        }

        /// <summary>
        /// Determine whether the currently running app has feature priority
        /// over all the others in the priority list.
        /// </summary>
        public async Task<bool> HaveFeaturePriorityAsync() {
            string packageFamilyName;
            Uri uriScheme;

            foreach (Tuple<string, Uri> tuple in this.priorityList) {
                packageFamilyName = tuple.Item1;
                uriScheme = tuple.Item2;

                if (await this.scanner.IsAppInstalledAsync(uriScheme, packageFamilyName)) {
                    return packageFamilyName == this.package.Current.Id.FamilyName;
                }
            }
            return false;
        }
    }
}
