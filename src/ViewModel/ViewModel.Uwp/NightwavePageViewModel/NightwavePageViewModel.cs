// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using Humanizer;
using ReactiveUI;
using Wfa.Provider.Interfaces;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;
using Wfa.ViewModel.Interfaces;
using Wfa.ViewModel.StateItems;
using Windows.UI.Xaml;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 午夜电波页面视图模型.
    /// </summary>
    public sealed partial class NightwavePageViewModel : ViewModelBase, IPageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NightwavePageViewModel"/> class.
        /// </summary>
        public NightwavePageViewModel(
            IStateProvider stateProvider,
            IResourceToolkit resourceToolkit)
        {
            _stateProvider = stateProvider;
            _resourceToolkit = resourceToolkit;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(500);
            _timer.Tick += OnTimerTick;

            ActiveCommand = ReactiveCommand.Create(Active);
            DeactiveCommand = ReactiveCommand.Create(Deactive);
            Challenges = new ObservableCollection<NightwaveItemViewModel>();
        }

        private void Active()
        {
            _stateProvider.StateChanged += OnStateChanged;
            InitializeData();
            _timer.Start();
        }

        private void Deactive()
        {
            _stateProvider.StateChanged -= OnStateChanged;
            _timer.Stop();
        }

        private void InitializeData()
        {
            var nightwave = _stateProvider.GetNightwave();

            if (nightwave == null)
            {
                return;
            }

            if (!(nightwave?.Challenges.Any() ?? false))
            {
                IsLoading = nightwave == null;
                IsEmpty = nightwave != null && nightwave.Challenges.Count() == 0;
                return;
            }

            var seasonFormat = _resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.SeasonFormat);
            Season = string.Format(seasonFormat, nightwave.Season);
            _expiryTime = nightwave.ExpiryTime.ToLocalTime();
            var expiryTimeFormat = _resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.EndDateFormat);
            ExpiryTime = string.Format(expiryTimeFormat, _expiryTime.Humanize());

            var newsCount = nightwave.Challenges.Count(p => !Challenges.Any(j => j.Data.Equals(p)));
            if (newsCount > 0)
            {
                TryClear(Challenges);
                nightwave.Challenges.OrderBy(p => p.Reputation).ThenBy(p => p.IsDaily).ToList().ForEach(p => Challenges.Add(new NightwaveItemViewModel(p)));
            }
            else
            {
                foreach (var item in nightwave.Challenges)
                {
                    var source = Challenges.FirstOrDefault(p => p.Data.Equals(item));
                    source?.UpdateDataCommand?.Execute(item).Subscribe();
                }
            }
        }

        private void OnStateChanged(object sender, EventArgs e)
            => InitializeData();

        private void OnTimerTick(object sender, object e)
        {
            var expiryTimeFormat = _resourceToolkit.GetLocaleString(Models.Enums.LanguageNames.EndDateFormat);
            ExpiryTime = string.Format(expiryTimeFormat, _expiryTime.Humanize());
        }
    }
}
