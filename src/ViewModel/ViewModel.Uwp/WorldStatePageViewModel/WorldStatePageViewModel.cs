﻿// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using Splat;
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
        }

        private void InitializeNews()
        {
            var news = _stateProvider.GetNews();
            if (!news?.Any() ?? true)
            {
                return;
            }

            var newCount = news.Count(p => !News.Any(j => j.Data.Equals(p)));
            if (newCount > 0)
            {
                // 有新的新闻传入，此时整体刷新.
                TryClear(News);
                news.OrderByDescending(p => p.Date).ToList().ForEach(p => News.Add(new NewsItemViewModel(p)));
                NewsCount = News.Count;
            }
        }

        private void InitializeEvents()
        {
            var events = _stateProvider.GetEvents();
            if (!events?.Any() ?? true)
            {
                IsEventsEmpty = true;
                TryClear(Events);
                return;
            }

            var newCount = events.Count(p => !Events.Any(j => j.Data.Equals(p)));
            if (newCount > 0)
            {
                TryClear(Events);
                events.ToList().ForEach(p => Events.Add(new EventItemViewModel(p)));
            }
            else
            {
                foreach (var newEvent in events)
                {
                    var sourceEvent = Events.FirstOrDefault(p => p.Data.Equals(newEvent));
                    sourceEvent?.UpdateDataCommand.Execute(newEvent).Subscribe();
                }
            }

            IsEventsEmpty = Events.Count == 0;
        }

        private void InitializeConstructionProgress()
        {
            var progress = _stateProvider.GetConstructionProgress();
            if (progress == null)
            {
                return;
            }

            if (ConstructionProgress == null)
            {
                ConstructionProgress = new ConstructionProgressViewModel(progress);
            }
            else
            {
                ConstructionProgress.UpdateDataCommand.Execute(progress).Subscribe();
            }
        }

        private void InitializePlains()
        {
            var zariman = _stateProvider.GetZarimanStatus();
            var cambion = _stateProvider.GetCambionStatus();
            var vallis = _stateProvider.GetVallisStatus();
            var cetus = _stateProvider.GetCetusStatus();
            var earth = _stateProvider.GetEarthStatus();
            AddOrUpdatePlainIntoCollection(zariman);
            AddOrUpdatePlainIntoCollection(cambion);
            AddOrUpdatePlainIntoCollection(vallis);
            AddOrUpdatePlainIntoCollection(cetus);
            AddOrUpdatePlainIntoCollection(earth);
        }

        private void AddOrUpdatePlainIntoCollection(object data)
        {
            if (data == null)
            {
                return;
            }

            var source = Cycles.FirstOrDefault(p => p.Data.GetType().Equals(data.GetType()));
            if (source != null)
            {
                source.UpdateDataCommand.Execute(data).Subscribe();
            }
            else
            {
                var vm = Locator.Current.GetService<WorldCycleItemViewModel>();
                vm.UpdateDataCommand.Execute(data).Subscribe();
                Cycles.Add(vm);
            }
        }

        private void OnWorldStateChanged(object sender, EventArgs e)
            => InitializeData();

        private void OnTimerTick(object sender, object e)
        {
            if (Cycles.Count > 0)
            {
                foreach (var cycle in Cycles)
                {
                    cycle.UpdateCountdownCommand.Execute().Subscribe();
                }
            }
        }
    }
}