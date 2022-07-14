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
    public sealed partial class ArchwingView : CenterPopup
    {
        /// <summary>
        /// <see cref="ViewModel"/> 的依赖属性.
        /// </summary>
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(ArchwingItemViewModel), typeof(ArchwingView), new PropertyMetadata(default));

        /// <summary>
        /// Initializes a new instance of the <see cref="ArchwingView"/> class.
        /// </summary>
        public ArchwingView() => InitializeComponent();

        /// <summary>
        /// 视图模型.
        /// </summary>
        public ArchwingItemViewModel ViewModel
        {
            get { return (ArchwingItemViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        /// <summary>
        /// 显示.
        /// </summary>
        /// <param name="data">数据.</param>
        public void Show(Archwing data)
        {
            Show();
            ViewModel = Locator.Current.GetService<ArchwingItemViewModel>();
            ViewModel.InitializeCommand.Execute(data);
        }
    }
}
