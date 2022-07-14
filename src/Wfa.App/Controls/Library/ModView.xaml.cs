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
    /// Mod 信息视图.
    /// </summary>
    public sealed partial class ModView : CenterPopup
    {
        /// <summary>
        /// <see cref="ViewModel"/> 的依赖属性.
        /// </summary>
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(ModItemViewModel), typeof(MeleeView), new PropertyMetadata(default));

        /// <summary>
        /// Initializes a new instance of the <see cref="ModView"/> class.
        /// </summary>
        public ModView() => InitializeComponent();

        /// <summary>
        /// 视图模型.
        /// </summary>
        public ModItemViewModel ViewModel
        {
            get { return (ModItemViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        /// <summary>
        /// 显示.
        /// </summary>
        /// <param name="data">数据.</param>
        public void Show(Mod data)
        {
            Show();
            ViewModel = Locator.Current.GetService<ModItemViewModel>();
            ViewModel.InitializeCommand.Execute(data);
        }
    }
}
