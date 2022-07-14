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
    public sealed partial class ArchMeleeView : CenterPopup
    {
        /// <summary>
        /// <see cref="ViewModel"/> 的依赖属性.
        /// </summary>
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(ArchMeleeItemViewModel), typeof(ArchMeleeView), new PropertyMetadata(default));

        /// <summary>
        /// Initializes a new instance of the <see cref="ArchMeleeView"/> class.
        /// </summary>
        public ArchMeleeView() => InitializeComponent();

        /// <summary>
        /// 视图模型.
        /// </summary>
        public ArchMeleeItemViewModel ViewModel
        {
            get { return (ArchMeleeItemViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        /// <summary>
        /// 显示.
        /// </summary>
        /// <param name="data">数据.</param>
        public void Show(ArchMelee data)
        {
            Show();
            ViewModel = Locator.Current.GetService<ArchMeleeItemViewModel>();
            ViewModel.InitializeCommand.Execute(data);
        }
    }
}
