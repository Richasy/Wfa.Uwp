// Copyright (c) Richasy. All rights reserved.

using System;
using System.Linq;
using Splat;
using Wfa.ViewModel.Items;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 世界状态页面视图模型.
    /// </summary>
    public sealed partial class WorldStatePageViewModel
    {
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

        private void InitializeDailySale()
        {
            var data = _stateProvider.GetDailySale();
            if (data == null)
            {
                return;
            }

            if (DailySale == null)
            {
                DailySale = new DailySaleViewModel(data);
            }
            else
            {
                DailySale.UpdateDataCommand.Execute(data).Subscribe();
            }
        }

        private void InitializeVoidTrader()
        {
            var data = _stateProvider.GetVoidTrader();
            if (data == null)
            {
                return;
            }

            if (VoidTrader == null)
            {
                VoidTrader = new VoidTraderViewModel(data);
            }
            else
            {
                VoidTrader.UpdateDataCommand.Execute(data).Subscribe();
            }
        }

        private void InitializeArbitration()
        {
            var data = _stateProvider.GetArbitration();
            if (data == null)
            {
                return;
            }

            if (Arbitration == null)
            {
                Arbitration = new ArbitrationViewModel(data);
            }
            else
            {
                Arbitration.UpdateDataCommand.Execute(data).Subscribe();
            }
        }

        private void InitializeSkirmish()
        {
            var data = _stateProvider.GetSkirmish();
            if (data == null)
            {
                return;
            }

            if (Skirmish == null)
            {
                Skirmish = new SkirmishViewModel(data);
            }
            else
            {
                Skirmish.UpdateDataCommand.Execute(data).Subscribe();
            }
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

            DailySale?.UpdateCountdownCommand.Execute().Subscribe();
            VoidTrader?.UpdateCountdownCommand.Execute().Subscribe();
            Arbitration?.UpdateCountdownCommand.Execute().Subscribe();
        }
    }
}
