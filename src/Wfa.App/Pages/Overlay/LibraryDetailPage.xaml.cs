// Copyright (c) Richasy. All rights reserved.

using System;
using Wfa.ViewModel;
using Wfa.ViewModel.LibraryItems;
using Windows.UI.Xaml.Navigation;

namespace Wfa.App.Pages.Overlay
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LibraryDetailPage : LibraryDetailPageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryDetailPage"/> class.
        /// </summary>
        public LibraryDetailPage() => InitializeComponent();

        /// <inheritdoc/>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is LibrarySectionViewModel vm)
            {
                ViewModel.SetDataType(vm);
            }

            base.OnNavigatedTo(e);
        }

        private void OnSearchSubmitted(Windows.UI.Xaml.Controls.AutoSuggestBox sender, Windows.UI.Xaml.Controls.AutoSuggestBoxQuerySubmittedEventArgs args)
            => ViewModel.SearchCommand.Execute(sender.Text).Subscribe();
    }

    /// <summary>
    /// <see cref="LibraryDetailPage"/> 的基类.
    /// </summary>
    public class LibraryDetailPageBase : AppPage<LibraryDetailPageViewModel>
    {
    }
}
