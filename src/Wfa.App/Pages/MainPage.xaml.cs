// Copyright (c) Richasy. All rights reserved.

using System;
using Windows.UI.Xaml;

namespace Wfa.App.Pages
{
    /// <summary>
    /// The page is used for default loading.
    /// </summary>
    public sealed partial class MainPage : AppPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            Loaded += OnLoadedAsync;
        }

        private async void OnLoadedAsync(object sender, RoutedEventArgs e)
        {
            await CoreViewModel.InitializeLanguageAsync();
            CoreViewModel.CheckLibraryDatabaseCommand.Execute().Subscribe();
            CoreViewModel.CheckWarframeMarketDatabaseCommand.Execute().Subscribe();
        }
    }
}
