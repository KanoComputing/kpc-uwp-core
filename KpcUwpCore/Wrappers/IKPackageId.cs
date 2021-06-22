/**
 * IKPackageId.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


namespace KanoComputing.KpcUwpCore.Wrappers {

    public interface IKPackageId {

        string FamilyName { get; }
        string FullName { get; }
        string Name { get; }
        string Publisher { get; }
        string PublisherId { get; }
        string ResourceId { get; }
    }
}
