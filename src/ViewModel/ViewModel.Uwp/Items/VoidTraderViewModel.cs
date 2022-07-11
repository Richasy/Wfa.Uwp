// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Humanizer;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.State;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.Items
{
    /// <summary>
    /// 虚空商人视图模型.
    /// </summary>
    public sealed class VoidTraderViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VoidTraderViewModel"/> class.
        /// </summary>
        public VoidTraderViewModel(VoidTrader data)
        {
            Items = new ObservableCollection<VoidTraderItem>();
            UpdateData(data);
            UpdateDataCommand = ReactiveCommand.Create<VoidTrader>(UpdateData);
            UpdateCountdownCommand = ReactiveCommand.Create(UpdateCountdown);
        }

        /// <summary>
        /// 更新数据命令.
        /// </summary>
        public ReactiveCommand<VoidTrader, Unit> UpdateDataCommand { get; }

        /// <summary>
        /// 更新倒计时命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> UpdateCountdownCommand { get; }

        /// <summary>
        /// 物品清单.
        /// </summary>
        public ObservableCollection<VoidTraderItem> Items { get; }

        /// <summary>
        /// 是否有物品清单.
        /// </summary>
        [Reactive]
        public bool HasItems { get; set; }

        /// <summary>
        /// 是否已经抵达.
        /// </summary>
        [Reactive]
        public bool IsArrived { get; set; }

        /// <summary>
        /// 倒计时.
        /// </summary>
        [Reactive]
        public string Countdown { get; set; }

        /// <summary>
        /// 源数据.
        /// </summary>
        [Reactive]
        public VoidTrader Data { get; set; }

        private void UpdateData(VoidTrader data)
        {
            Data = data;
            if ((data.Inventory?.Any() ?? false) && Items.Count == 0)
            {
                foreach (var item in data.Inventory)
                {
                    Items.Add(item);
                }
            }
            else
            {
                TryClear(Items);
                HasItems = false;
            }

            IsArrived = data.IsActive;
            HasItems = Items.Count > 0;
            UpdateCountdown();
        }

        private void UpdateCountdown()
        {
            var time = Data.IsActive
                ? Data.ExpiryTime.ToLocalTime()
                : Data.StartTime.ToLocalTime();
            if (time <= DateTime.Now)
            {
                Countdown = TimeSpan.Zero.Humanize(minUnit: Humanizer.Localisation.TimeUnit.Second);
                return;
            }

            Countdown = (time - DateTime.Now).Humanize(maxUnit: Humanizer.Localisation.TimeUnit.Day, minUnit: Humanizer.Localisation.TimeUnit.Second);
        }
    }
}
