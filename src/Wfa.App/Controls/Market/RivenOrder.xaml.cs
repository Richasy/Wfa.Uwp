// Copyright (c) Richasy. All rights reserved.

using System;
using ReactiveUI;
using Wfa.ViewModel.MarketItems;

namespace Wfa.App.Controls.Market
{
    /// <summary>
    /// 紫卡订单.
    /// </summary>
    public sealed partial class RivenOrder : RivenOrderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RivenOrder"/> class.
        /// </summary>
        public RivenOrder() => InitializeComponent();

        private void OnProfileTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
            => ViewModel.GotoProfileCommand.Execute().Subscribe();
    }

    /// <summary>
    /// <see cref="RivenOrder"/> 的基类.
    /// </summary>
    public class RivenOrderBase : ReactiveUserControl<RivenOrderViewModel>
    {
    }
}
