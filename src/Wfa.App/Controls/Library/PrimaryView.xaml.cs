// Copyright (c) Richasy. All rights reserved.

using System;
using Splat;
using Wfa.App.Controls.App;
using Wfa.Models.Community;
using Wfa.Models.Market;
using Wfa.ViewModel.LibraryItems;
using Windows.UI.Xaml;

namespace Wfa.App.Controls.Library
{
    /// <summary>
    /// 战甲信息视图.
    /// </summary>
    public sealed partial class PrimaryView : CenterPopup
    {
        /// <summary>
        /// <see cref="ViewModel"/> 的依赖属性.
        /// </summary>
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(PrimaryItemViewModel), typeof(PrimaryView), new PropertyMetadata(default));

        /// <summary>
        /// Initializes a new instance of the <see cref="PrimaryView"/> class.
        /// </summary>
        public PrimaryView() => InitializeComponent();

        /// <summary>
        /// 视图模型.
        /// </summary>
        public PrimaryItemViewModel ViewModel
        {
            get { return (PrimaryItemViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        /// <summary>
        /// 显示.
        /// </summary>
        /// <param name="data">数据.</param>
        public void Show(Primary data)
        {
            Show();
            ViewModel = Locator.Current.GetService<PrimaryItemViewModel>();
            ViewModel.InitializeCommand.Execute(data);
        }

        private void OnMarketItemClick(object sender, RoutedEventArgs e)
        {
            var data = (sender as FrameworkElement).DataContext as MarketItem;
            ViewModel.JumpToMarketCommand.Execute(data).Subscribe();
        }
    }
}
