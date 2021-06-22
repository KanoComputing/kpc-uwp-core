/**
 * IKPackage.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


namespace KanoComputing.KpcUwpCore.Wrappers {

    public interface IKPackage {

        IKPackage Current { get; }
        IKPackageId Id { get; }
    }
}
