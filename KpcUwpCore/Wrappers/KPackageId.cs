/**
 * KPackageId.cs
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
    public class KPackageId : IKPackageId {

        private readonly PackageId id;

        public KPackageId(PackageId id) {
            this.id = id;
        }

        public string FamilyName {
            get { return this.id.FamilyName; }
        }
        public string FullName {
            get { return this.id.FullName; }
        }
        public string Name {
            get { return this.id.Name; }
        }
        public string Publisher {
            get { return this.id.Publisher; }
        }
        public string PublisherId {
            get { return this.id.PublisherId; }
        }
        public string ResourceId {
            get { return this.id.ResourceId; }
        }
    }
}
