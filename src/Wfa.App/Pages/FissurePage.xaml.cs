// Copyright (c) Richasy. All rights reserved.

using System;
using Wfa.ViewModel;
using Windows.UI.Xaml.Navigation;

namespace Wfa.App.Pages
{
    /// <summary>
    /// 虚空裂缝页面.
    /// </summary>
    public sealed partial class FissurePage : FissurePageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FissurePage"/> class.
        /// </summary>
        public FissurePage() => InitializeComponent();

        /// <inheritdoc/>
        protected override void OnNavigatedTo(NavigationEventArgs e)
            => ViewModel.ActiveCommand.Execute().Subscribe();

        /// <inheritdoc/>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
            => ViewModel.DeactiveCommand.Execute().Subscribe();
    }

    /// <summary>
    /// <see cref="FissurePage"/> 的基类.
    /// </summary>
    public class FissurePageBase : AppPage<FissurePageViewModel>
    {
    }
}
