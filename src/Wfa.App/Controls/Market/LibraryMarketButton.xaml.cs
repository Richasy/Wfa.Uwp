// Copyright (c) Richasy. All rights reserved.

using System;
using ReactiveUI;
using Wfa.Models.Market;
using Wfa.ViewModel.Base;
using Wfa.ViewModel.MarketItems;
using Windows.UI.Xaml;

namespace Wfa.App.Controls.Market
{
    /// <summary>
    /// 资料库商店按钮.
    /// </summary>
    public sealed partial class LibraryMarketButton : LibraryMarketButtonBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryMarketButton"/> class.
        /// </summary>
        public LibraryMarketButton() => InitializeComponent();

        private void OnMarketItemClick(object sender, RoutedEventArgs e)
        {
            var data = (sender as FrameworkElement).DataContext as MarketSuggestionItemViewModel;
            ViewModel.JumpToMarketCommand.Execute(data.Data as MarketItem).Subscribe();
        }
    }

    /// <summary>
    /// <see cref="LibraryMarketButton"/> 的基类.
    /// </summary>
    public class LibraryMarketButtonBase : ReactiveUserControl<EntryViewModelBase>
    {
    }
}
