/**
* IWindowManager.cs
*
* Copyright (c) 2020 Kano Computing Ltd.
* License: https://opensource.org/licenses/MIT
*/


using Windows.Foundation;


namespace KanoComputing.KpcUwpCore.Display {

    public interface IWindowManager {

        Size GetEffectiveScreenSize();
        void MaximiseWindow();
        void SetWindowSize(Size size);
    }
}
