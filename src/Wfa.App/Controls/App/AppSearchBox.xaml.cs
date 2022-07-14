// Copyright (c) Richasy. All rights reserved.

using System;
using ReactiveUI;
using Splat;
using Wfa.ViewModel;
using Wfa.ViewModel.MarketItems;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Wfa.App.Controls.App
{
    /// <summary>
    /// 应用搜索框.
    /// </summary>
    public sealed partial class AppSearchBox : AppSearchBoxBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppSearchBox"/> class.
        /// </summary>
        public AppSearchBox()
        {
            InitializeComponent();
            ViewModel = Locator.Current.GetService<MarketSearchBoxViewModel>();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
            => ViewModel.InitializeCommand.Execute().Subscribe();

        private void OnSearchBoxSubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion is MarketSuggestionItemViewModel vm)
            {
                ViewModel.GotoOrderPageCommand.Execute(vm).Subscribe();
            }
        }
    }

    /// <summary>
    /// <see cref="AppSearchBox"/> 的基类.
    /// </summary>
    public class AppSearchBoxBase : ReactiveUserControl<MarketSearchBoxViewModel>
    {
    }
}
