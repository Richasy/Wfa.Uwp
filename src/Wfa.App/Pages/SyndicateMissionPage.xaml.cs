// Copyright (c) Richasy. All rights reserved.

using System;
using Wfa.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace Wfa.App.Pages
{
    /// <summary>
    /// 赏金任务页面.
    /// </summary>
    public sealed partial class SyndicateMissionPage : SyndicateMissionPageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SyndicateMissionPage"/> class.
        /// </summary>
        public SyndicateMissionPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            SizeChanged += OnSizeChanged;
        }

        /// <inheritdoc/>
        protected override void OnNavigatedTo(NavigationEventArgs e)
            => ViewModel.ActiveCommand.Execute().Subscribe();

        /// <inheritdoc/>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
            => ViewModel.DeactiveCommand.Execute().Subscribe();

        private void OnLoaded(object sender, RoutedEventArgs e)
            => InitializeWidth();

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
            => InitializeWidth();

        private void InitializeWidth()
        {
            var pageContentWidth = ActualWidth - CoreViewModel.PageHorizontalPadding.Left - CoreViewModel.PageHorizontalPadding.Right;
            ContentContainer.Width = pageContentWidth;
        }
    }

    /// <summary>
    /// <see cref="SyndicateMissionPage"/> 的基类.
    /// </summary>
    public class SyndicateMissionPageBase : AppPage<SyndicateMissionPageViewModel>
    {
    }
}
