// Copyright (c) Richasy. All rights reserved.

using System;
using Wfa.ViewModel;
using Windows.UI.Xaml.Navigation;

namespace Wfa.App.Pages
{
    /// <summary>
    /// 世界状态首页.
    /// </summary>
    public sealed partial class WorldStatePage : WorldStatePageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldStatePage"/> class.
        /// </summary>
        public WorldStatePage() => InitializeComponent();

        /// <inheritdoc/>
        protected override void OnNavigatedTo(NavigationEventArgs e)
            => ViewModel.ActiveCommand.Execute().Subscribe();

        /// <inheritdoc/>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
            => ViewModel.DeactiveCommand.Execute().Subscribe();
    }

    /// <summary>
    /// <see cref="WorldStatePage"/> 的基类.
    /// </summary>
    public class WorldStatePageBase : AppPage<WorldStatePageViewModel>
    {
    }
}
