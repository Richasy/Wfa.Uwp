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
    /// 赏金任务页面视图模型.
    /// </summary>
    public sealed partial class SyndicateMissionPageViewModel
    {
        private readonly IStateProvider _stateProvider;
        private readonly DispatcherTimer _timer;

        /// <inheritdoc/>
        public ReactiveCommand<Unit, Unit> ActiveCommand { get; }

        /// <inheritdoc/>
        public ReactiveCommand<Unit, Unit> DeactiveCommand { get; }

        /// <summary>
        /// 赏金列表.
        /// </summary>
        public ObservableCollection<SyndicateMissionViewModel> Missions { get; }

        /// <summary>
        /// 当前选中的赏金任务.
        /// </summary>
        [Reactive]
        public SyndicateMissionViewModel CurrentMission { get; set; }

        /// <summary>
        /// 是否正在加载.
        /// </summary>
        [Reactive]
        public bool IsLoading { get; set; }
    }
}
