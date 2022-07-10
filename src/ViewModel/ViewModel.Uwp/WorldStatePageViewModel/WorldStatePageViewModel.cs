// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.ObjectModel;
using Wfa.Provider.Interfaces;
using Wfa.ViewModel.Base;
using Wfa.ViewModel.Items;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 世界状态首页视图模型.
    /// </summary>
    /// <remarks>
    /// 应包含的模块：世界周期, 游戏新闻，商人，活动，仲裁，前哨战，战舰建造进度.
    /// </remarks>
    public sealed partial class WorldStatePageViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldStatePageViewModel"/> class.
        /// </summary>
        public WorldStatePageViewModel(
            IStateProvider stateProvider)
        {
            _stateProvider = stateProvider;
            _timer = new Windows.UI.Xaml.DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(500);
            _timer.Tick += OnTimerTick;
            News = new ObservableCollection<NewsItemViewModel>();
            Events = new ObservableCollection<EventItemViewModel>();
            Cycles = new ObservableCollection<WorldCycleItemViewModel>();

            _stateProvider.StateChanged += OnWorldStateChanged;
            InitializeData();
        }

        private void InitializeData()
        {
            if (!_timer.IsEnabled)
            {
                _timer.Start();
            }

            InitializeNews();
            InitializeEvents();
            InitializeConstructionProgress();
            InitializePlains();
            InitializeDailySale();
        }
    }
}
