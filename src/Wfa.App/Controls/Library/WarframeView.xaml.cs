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
    public sealed partial class WarframeView : CenterPopup
    {
        /// <summary>
        /// <see cref="ViewModel"/> 的依赖属性.
        /// </summary>
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(WarframeItemViewModel), typeof(WarframeView), new PropertyMetadata(default));

        /// <summary>
        /// Initializes a new instance of the <see cref="WarframeView"/> class.
        /// </summary>
        public WarframeView() => InitializeComponent();

        /// <summary>
        /// 视图模型.
        /// </summary>
        public WarframeItemViewModel ViewModel
        {
            get { return (WarframeItemViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        /// <summary>
        /// 显示.
        /// </summary>
        /// <param name="data">数据.</param>
        public void Show(Warframe data)
        {
            Show();
            ViewModel = Locator.Current.GetService<WarframeItemViewModel>();
            ViewModel.InitializeCommand.Execute(data);
        }
    }
}
