// Copyright (c) Richasy. All rights reserved.

using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.Provider.Interfaces;
using Wfa.ViewModel.StateItems;
using Windows.UI.Xaml;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 虚空裂缝页面视图模型.
    /// </summary>
    public sealed partial class FissurePageViewModel
    {
        private readonly IStateProvider _stateProvider;
        private readonly DispatcherTimer _timer;

        /// <inheritdoc/>
        public ReactiveCommand<Unit, Unit> ActiveCommand { get; }

        /// <inheritdoc/>
        public ReactiveCommand<Unit, Unit> DeactiveCommand { get; }

        /// <summary>
        /// 入侵集合.
        /// </summary>
        public ObservableCollection<FissureItemViewModel> Fissures { get; }

        /// <summary>
        /// 是否正在加载中.
        /// </summary>
        [Reactive]
        public bool IsLoading { get; set; }

        /// <summary>
        /// 是否为空.
        /// </summary>
        [Reactive]
        public bool IsEmpty { get; set; }
    }
}
