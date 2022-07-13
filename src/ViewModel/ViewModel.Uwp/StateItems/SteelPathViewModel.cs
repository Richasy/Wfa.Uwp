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

namespace Wfa.ViewModel.StateItems
{
    /// <summary>
    /// 钢铁之路视图模型.
    /// </summary>
    public sealed class SteelPathViewModel : ViewModelBase, ICountdownViewModel
    {
        private DateTime _expiryTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="SteelPathViewModel"/> class.
        /// </summary>
        public SteelPathViewModel(SteelPath data)
        {
            Rewards = new ObservableCollection<SteelPathRewardItemViewModel>();
            UpdateCountdownCommand = ReactiveCommand.Create(UpdateCountdown);
            UpdateDataCommand = ReactiveCommand.Create<SteelPath>(UpdateData);
            UpdateData(data);
        }

        /// <summary>
        /// 更新数据命令.
        /// </summary>
        public ReactiveCommand<SteelPath, Unit> UpdateDataCommand { get; }

        /// <summary>
        /// 更新倒计时的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> UpdateCountdownCommand { get; }

        /// <summary>
        /// 每周轮换奖励列表.
        /// </summary>
        public ObservableCollection<SteelPathRewardItemViewModel> Rewards { get; }

        /// <summary>
        /// 过期提示.
        /// </summary>
        [Reactive]
        public string Countdown { get; set; }

        private void UpdateData(SteelPath data)
        {
            _expiryTime = data.ExpiryTime.ToLocalTime();

            var newsCount = data.Rotation.Count(data => !Rewards.Any(j => j.Data.Equals(data)));
            if (newsCount > 0)
            {
                var currentReward = data.CurrentReward;
                TryClear(Rewards);
                data.Rotation.ForEach(p => Rewards.Add(new SteelPathRewardItemViewModel(p, p.Equals(currentReward))));
            }

            UpdateCountdown();
        }

        private void UpdateCountdown()
        {
            if (_expiryTime == DateTime.MinValue
                || _expiryTime <= DateTime.Now)
            {
                Countdown = "--";
                return;
            }

            var expiryFormat = Locator.Current.GetService<IResourceToolkit>().GetLocaleString(LanguageNames.EndDateFormat);
            Countdown = string.Format(expiryFormat, _expiryTime.Humanize());
        }
    }
}
