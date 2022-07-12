﻿// Copyright (c) Richasy. All rights reserved.

using System;
using Wfa.ViewModel;
using Windows.UI.Xaml.Navigation;

namespace Wfa.App.Pages
{
    /// <summary>
    /// 资料库页面.
    /// </summary>
    public sealed partial class LibraryPage : LibraryPageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryPage"/> class.
        /// </summary>
        public LibraryPage() => InitializeComponent();

        /// <inheritdoc/>
        protected override void OnNavigatedTo(NavigationEventArgs e)
            => ViewModel.ActiveCommand.Execute().Subscribe();

        /// <inheritdoc/>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
            => ViewModel.DeactiveCommand.Execute().Subscribe();
    }

    /// <summary>
    /// <see cref="LibraryPage"/> 的基类.
    /// </summary>
    public class LibraryPageBase : AppPage<LibraryPageViewModel>
    {
    }
}
