// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Humanizer;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Wfa.Models.Enums;
using Wfa.Models.State;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;
using Wfa.ViewModel.Interfaces;

namespace Wfa.ViewModel.Items
{
    /// <summary>
    /// 赏金任务条目视图模型.
    /// </summary>
    public sealed class SyndicateMissionViewModel : ViewModelBase, ICountdownViewModel
    {
        private DateTime _expiryTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyndicateMissionViewModel"/> class.
        /// </summary>
        public SyndicateMissionViewModel(SyndicateMission mission)
        {
            Jobs = new ObservableCollection<SyndicateMissionJobViewModel>();
            UpdateData(mission);
            UpdateCountdownCommand = ReactiveCommand.Create(UpdateCountdown);
            UpdateDataCommand = ReactiveCommand.Create<SyndicateMission>(UpdateData);
        }

        /// <summary>
        /// 任务列表.
        /// </summary>
        public ObservableCollection<SyndicateMissionJobViewModel> Jobs { get; }

        /// <inheritdoc/>
        public ReactiveCommand<Unit, Unit> UpdateCountdownCommand { get; }

        /// <summary>
        /// 更新数据的命令.
        /// </summary>
        public ReactiveCommand<SyndicateMission, Unit> UpdateDataCommand { get; }

        /// <summary>
        /// 阵营名称.
        /// </summary>
        [Reactive]
        public string Name { get; set; }

        /// <inheritdoc/>
        [Reactive]
        public string Countdown { get; set; }

        private void UpdateCountdown()
        {
            if (_expiryTime == DateTime.MinValue || _expiryTime < DateTime.Now)
            {
                Countdown = "--";
                return;
            }

            var format = Locator.Current.GetService<IResourceToolkit>().GetLocaleString(Models.Enums.LanguageNames.ExpiryDateFormat);
            Countdown = string.Format(format, _expiryTime.Humanize());
        }

        private void UpdateData(SyndicateMission mission)
        {
            Name = mission.Name;
            var symbol = mission.Key switch
            {
                "Ostrons" => WfaSymbol.Ostron,
                "Solaris United" => WfaSymbol.Solaris,
                _ => WfaSymbol.Infested,
            };

            _expiryTime = mission.ExpiryTime.ToLocalTime();
            var newsCount = mission.Jobs.Count(p => !Jobs.Any(j => j.Id == p.Id));
            if (newsCount > 0)
            {
                // 赏金列表刷新了.
                TryClear(Jobs);
                mission.Jobs.ForEach(p => Jobs.Add(new SyndicateMissionJobViewModel(p, symbol)));
            }

            UpdateCountdown();
        }
    }
}
