// Copyright (c) Richasy. All rights reserved.

using System;
using System.ComponentModel;
using System.Threading.Tasks;
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
        private readonly AppViewModel _coreViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSearchBox"/> class.
        /// </summary>
        public AppSearchBox()
        {
            InitializeComponent();
            _coreViewModel = Locator.Current.GetService<AppViewModel>();
            ViewModel = Locator.Current.GetService<MarketSearchBoxViewModel>();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _coreViewModel.PropertyChanged += OnCoreViewModelPropertyChangedAsync;
            ViewModel.InitializeCommand.Execute().Subscribe();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
            => _coreViewModel.PropertyChanged -= OnCoreViewModelPropertyChangedAsync;

        private async void OnCoreViewModelPropertyChangedAsync(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_coreViewModel.IsLibraryUpdating))
            {
                if (!_coreViewModel.IsLibraryUpdating)
                {
                    await Task.Delay(500);
                    ViewModel.InitializeCommand.Execute().Subscribe();
                }
            }
        }

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
