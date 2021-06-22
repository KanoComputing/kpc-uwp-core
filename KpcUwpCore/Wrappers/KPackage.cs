/**
 * KPackage.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using Windows.ApplicationModel;


namespace KanoComputing.KpcUwpCore.Wrappers {

    /// <summary>
    /// Testable wrapper for Windows.ApplicationModel.Package.
    /// </summary>
    /// <remarks>
    /// Because the original object has no contructor and is a sealed class,
    /// this object only wraps functionality required so far and any extensions
    /// should be made as necessary.
    /// </remarks>
    public class KPackage : IKPackage {

        private Package package;

        public IKPackage Current {
            get {
                this.package = Package.Current;
                return this;
            }
        }

        public IKPackageId Id {
            get { return new KPackageId(this.package.Id); }
        }
    }
}
