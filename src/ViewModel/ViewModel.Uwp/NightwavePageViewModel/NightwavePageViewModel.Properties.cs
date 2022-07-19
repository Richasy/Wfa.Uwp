// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.Provider.Interfaces;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.StateItems;
using Windows.UI.Xaml;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 午夜电波页面视图模型.
    /// </summary>
    public sealed partial class NightwavePageViewModel
    {
        private readonly IStateProvider _stateProvider;
        private readonly IResourceToolkit _resourceToolkit;
        private readonly DispatcherTimer _timer;
        private DateTime _expiryTime;

        /// <inheritdoc/>
        public ReactiveCommand<Unit, Unit> ActiveCommand { get; }

        /// <inheritdoc/>
        public ReactiveCommand<Unit, Unit> DeactiveCommand { get; }

        /// <summary>
        /// 入侵集合.
        /// </summary>
        public ObservableCollection<NightwaveItemViewModel> Challenges { get; }

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

        /// <summary>
        /// 赛季名.
        /// </summary>
        [Reactive]
        public string Season { get; set; }

        /// <summary>
        /// 结束时间.
        /// </summary>
        [Reactive]
        public string ExpiryTime { get; set; }
    }
}
