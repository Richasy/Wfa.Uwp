// Copyright (c) Richasy. All rights reserved.

using System;
using ReactiveUI;
using Wfa.ViewModel.MarketItems;

namespace Wfa.App.Controls.Market
{
    /// <summary>
    /// 条目订单.
    /// </summary>
    public sealed partial class LichOrder : LichOrderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LichOrder"/> class.
        /// </summary>
        public LichOrder() => InitializeComponent();

        private void OnProfileTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
            => ViewModel.GotoProfileCommand.Execute().Subscribe();
    }

    /// <summary>
    /// <see cref="LichOrder"/> 的基类.
    /// </summary>
    public class LichOrderBase : ReactiveUserControl<LichOrderViewModel>
    {
    }
}
