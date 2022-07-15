// Copyright (c) Richasy. All rights reserved.

using System;
using Wfa.Models.Data.Local;
using Wfa.ViewModel;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Wfa.App.Pages
{
    /// <summary>
    /// 帮助与支持页面.
    /// </summary>
    public sealed partial class HelpPage : HelpPageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpPage"/> class.
        /// </summary>
        public HelpPage() => InitializeComponent();

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = Window.Current.Bounds.Width;
            if (width < CoreViewModel.MediumWindowThresholdWidth)
            {
                if (WideContainer.Children.Count > 0)
                {
                    WideContainer.Children.Clear();
                    NarrowScrollViewer.Visibility = Visibility.Visible;
                    WideContainer.Visibility = Visibility.Collapsed;
                    NarrowContainer.Children.Add(AboutContainer);
                    NarrowContainer.Children.Add(QuestionContainer);
                    Grid.SetColumn(QuestionContainer, 0);
                    Grid.SetRow(QuestionContainer, 1);
                }
            }
            else
            {
                if (NarrowContainer.Children.Count > 0)
                {
                    NarrowContainer.Children.Clear();
                    NarrowScrollViewer.Visibility = Visibility.Collapsed;
                    WideContainer.Visibility = Visibility.Visible;
                    WideContainer.Children.Add(AboutContainer);
                    WideContainer.Children.Add(QuestionContainer);
                    Grid.SetColumn(QuestionContainer, 1);
                    Grid.SetRow(QuestionContainer, 0);
                }
            }
        }

        private async void OnLinkViewItemClickAsync(object sender, ItemClickEventArgs e)
        {
            var data = e.ClickedItem as KeyValue;
            await Launcher.LaunchUriAsync(new Uri(data.Value));
        }
    }

    /// <summary>
    /// <see cref="HelpPage"/> 的基类.
    /// </summary>
    public class HelpPageBase : AppPage<HelpPageViewModel>
    {
    }
}
