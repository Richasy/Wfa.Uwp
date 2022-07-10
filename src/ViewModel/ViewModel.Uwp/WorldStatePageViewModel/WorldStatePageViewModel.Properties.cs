// Copyright (c) Richasy. All rights reserved.

using System.Collections.ObjectModel;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.State;
using Wfa.Provider.Interfaces;
using Wfa.ViewModel.Items;
using Windows.UI.Xaml;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 首页视图模型.
    /// </summary>
    public sealed partial class WorldStatePageViewModel
    {
        private readonly IStateProvider _stateProvider;
        private readonly DispatcherTimer _timer;

        /// <summary>
        /// 新闻.
        /// </summary>
        public ObservableCollection<NewsItemViewModel> News { get; }

        /// <summary>
        /// 活动列表.
        /// </summary>
        public ObservableCollection<EventItemViewModel> Events { get; }

        /// <summary>
        /// 平原列表.
        /// </summary>
        public ObservableCollection<WorldCycleItemViewModel> Cycles { get; }

        /// <summary>
        /// 每日商品 (Darvo).
        /// </summary>
        [Reactive]
        public DailySale DailySale { get; set; }

        /// <summary>
        /// 仲裁信息.
        /// </summary>
        [Reactive]
        public Arbitration Arbitration { get; set; }

        /// <summary>
        /// 建造进度.
        /// </summary>
        [Reactive]
        public ConstructionProgressViewModel ConstructionProgress { get; set; }

        /// <summary>
        /// 前哨战信息.
        /// </summary>
        [Reactive]
        public SentientBattle SentientBattle { get; set; }

        /// <summary>
        /// 虚空商人是否已经抵达.
        /// </summary>
        [Reactive]
        public bool IsVoidTraderArrived { get; set; }

        /// <summary>
        /// 活动是否为空.
        /// </summary>
        [Reactive]
        public bool IsEventsEmpty { get; set; }

        /// <summary>
        /// 新闻数.
        /// </summary>
        [Reactive]
        public int NewsCount { get; set; }
    }
}
