// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using ReactiveUI;
using Wfa.Provider.Interfaces;
using Wfa.ViewModel.Base;
using Wfa.ViewModel.Interfaces;
using Wfa.ViewModel.Items;
using Windows.UI.Xaml;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 虚空裂缝页面视图模型.
    /// </summary>
    public sealed partial class FissurePageViewModel : ViewModelBase, IPageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FissurePageViewModel"/> class.
        /// </summary>
        public FissurePageViewModel(IStateProvider stateProvider)
        {
            _stateProvider = stateProvider;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(500);
            _timer.Tick += OnTimerTick;

            ActiveCommand = ReactiveCommand.Create(Active);
            DeactiveCommand = ReactiveCommand.Create(Deactive);
            Fissures = new ObservableCollection<FissureItemViewModel>();
        }

        private void Active()
        {
            _stateProvider.StateChanged += OnStateChanged;
            InitializeData();
        }

        private void Deactive()
            => _stateProvider.StateChanged -= OnStateChanged;

        private void InitializeData()
        {
            var fissures = _stateProvider.GetFissures();

            IsLoading = fissures == null;
            if (!(fissures?.Any() ?? false))
            {
                IsLoading = fissures == null;
                IsEmpty = fissures != null && fissures.Count() == 0;
                return;
            }

            var newsCount = fissures.Count(p => !Fissures.Any(j => j.Data.Equals(p)));
            if (newsCount > 0)
            {
                TryClear(Fissures);
                fissures.ToList().ForEach(p => Fissures.Add(new FissureItemViewModel(p)));
            }
            else
            {
                foreach (var item in fissures.OrderBy(p => p.TierIndex))
                {
                    var source = Fissures.FirstOrDefault(p => p.Data.Equals(item));
                    source?.UpdateDataCommand?.Execute(item).Subscribe();
                }
            }
        }

        private void OnStateChanged(object sender, EventArgs e)
            => InitializeData();

        private void OnTimerTick(object sender, object e)
        {
            if (Fissures.Count == 0)
            {
                return;
            }

            foreach (var item in Fissures)
            {
                item.UpdateCountdownCommand.Execute().Subscribe();
            }
        }
    }
}
