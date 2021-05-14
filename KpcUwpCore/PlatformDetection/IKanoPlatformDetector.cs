/**
 * IKanoPlatformDetector.cs
 *
 * Copyright (c) 2020 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using KanoComputing.KpcUwpCore.Contracts;


namespace KanoComputing.KpcUwpCore.PlatformDetection {

    public interface IKanoPlatformDetector {

        bool IsKanoDevice();

        bool IsKanoPc();

        KanoDevice GetKanoDevice();
        KanoPcSku GetKanoPcSku();
    }
}
