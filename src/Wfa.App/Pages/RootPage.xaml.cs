// Copyright (c) Richasy. All rights reserved.

using System;
using Wfa.Models.Enums;
using Wfa.ViewModel;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Wfa.App.Pages
{
    /// <summary>
    /// The page is used for default loading.
    /// </summary>
    public sealed partial class RootPage : RootPageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RootPage"/> class.
        /// </summary>
        public RootPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            SizeChanged += OnSizeChanged;

            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
            => CoreViewModel.InitializePadding();

        private void OnLoaded(object sender, RoutedEventArgs e)
            => CoreViewModel.InitializePadding();

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;
            Back();
        }

        private void Back()
        {
            if (ViewModel.CanBack)
            {
                ViewModel.BackCommand.Execute().Subscribe();
            }
        }

        private async void OnNavViewFirstLoadedAsync(object sender, EventArgs e)
        {
            ViewModel.NavigateToMainView(PageIds.WorldStateHome);
            await CoreViewModel.InitializeLanguageAsync();
            CoreViewModel.CheckLibraryDatabaseCommand.Execute().Subscribe();
            CoreViewModel.CheckWarframeMarketDatabaseCommand.Execute().Subscribe();
            CoreViewModel.CheckTranslateDatabaseCommand.Execute().Subscribe();
            CoreViewModel.CheckPatchDatabaseCommand.Execute().Subscribe();
            CoreViewModel.BeginLoopWorldStateCommand.Execute().Subscribe();
        }
    }

    /// <summary>
    /// <see cref="RootPage"/> 的基类.
    /// </summary>
    public class RootPageBase : AppPage<NavigationViewModel>
    {
    }
}
