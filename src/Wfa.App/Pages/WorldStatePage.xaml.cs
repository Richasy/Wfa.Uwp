// Copyright (c) Richasy. All rights reserved.

using Wfa.ViewModel;
using Windows.UI.Xaml;

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
        public WorldStatePage()
        {
            InitializeComponent();
            SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = Window.Current.Bounds.Width;
            var pageContentWidth = e.NewSize.Width - CoreViewModel.PageHorizontalPadding.Left - CoreViewModel.PageHorizontalPadding.Right;
            NewsPanel.Width = width < CoreViewModel.MediumWindowThresholdWidth
                ? pageContentWidth
                : SecondaryColumn.Width.Value;
            MainContainer.Width = width < CoreViewModel.MediumWindowThresholdWidth
                ? pageContentWidth
                : pageContentWidth - SecondaryColumn.Width.Value - 24;
        }
    }

    /// <summary>
    /// <see cref="WorldStatePage"/> 的基类.
    /// </summary>
    public class WorldStatePageBase : AppPage<WorldStatePageViewModel>
    {
    }
}
