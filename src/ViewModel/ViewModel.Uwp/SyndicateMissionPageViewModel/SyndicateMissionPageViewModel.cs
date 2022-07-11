// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using ReactiveUI;
using Wfa.Models.State;
using Wfa.Provider.Interfaces;
using Wfa.ViewModel.Base;
using Wfa.ViewModel.Interfaces;
using Wfa.ViewModel.Items;
using Windows.UI.Xaml;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 赏金任务页面视图模型.
    /// </summary>
    public sealed partial class SyndicateMissionPageViewModel : ViewModelBase, IPageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SyndicateMissionPageViewModel"/> class.
        /// </summary>
        public SyndicateMissionPageViewModel(IStateProvider stateProvider)
        {
            _stateProvider = stateProvider;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(500);
            _timer.Tick += OnTimerTick;
            _stateProvider.StateChanged += OnStateChanged;

            ActiveCommand = ReactiveCommand.Create(Active);
            DeactiveCommand = ReactiveCommand.Create(Deactive);
            Missions = new ObservableCollection<SyndicateMissionViewModel>();
        }

        private void Active()
        {
            _stateProvider.StateChanged += OnStateChanged;
            InitializeData();
        }

        private void Deactive()
        {
            _stateProvider.StateChanged -= OnStateChanged;
            _timer.Stop();
        }

        private void InitializeData()
        {
            if (!_timer.IsEnabled)
            {
                _timer.Start();
            }

            var ostron = _stateProvider.GetOstronSyndicateMission();

            IsLoading = ostron == null;
            if (IsLoading)
            {
                return;
            }

            var solaris = _stateProvider.GetSolarisSyndicateMission();
            var entrati = _stateProvider.GetEntratiSyndicateMission();

            AddOrUpdateMission(ostron);
            AddOrUpdateMission(solaris);
            AddOrUpdateMission(entrati);
        }

        private void AddOrUpdateMission(SyndicateMission mission)
        {
            if (mission == null)
            {
                return;
            }

            var sourceMission = Missions.FirstOrDefault(p => p.Name == mission.Name);
            if (sourceMission != null)
            {
                sourceMission.UpdateDataCommand.Execute(mission).Subscribe();
            }
            else
            {
                Missions.Add(new SyndicateMissionViewModel(mission));
            }
        }

        private void OnStateChanged(object sender, EventArgs e)
            => InitializeData();

        private void OnTimerTick(object sender, object e)
        {
            if (Missions.Count == 0)
            {
                return;
            }

            foreach (var item in Missions)
            {
                item.UpdateCountdownCommand.Execute().Subscribe();
            }
        }
    }
}
