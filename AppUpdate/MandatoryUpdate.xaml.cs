﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace AppUpdate {

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MandatoryUpdate : Page {
        public MandatoryUpdate() {
            this.InitializeComponent();
        }

        private void OnUpdateButtonClicked(object sender, RoutedEventArgs e) {
            // Launch the Microsoft Store to the Downloads and Updates page.
            _ = Windows.System.Launcher.LaunchUriAsync(
                new Uri("ms-windows-store://downloadsandupdates")
            );
        }
    }
}