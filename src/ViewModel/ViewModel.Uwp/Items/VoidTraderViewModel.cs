// Copyright (c) Richasy. All rights reserved.

using System;
using System.Linq;
using System.Reactive;
using Humanizer;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Wfa.Models.State;
using Wfa.ViewModel.Base;
using Wfa.ViewModel.Interfaces;

namespace Wfa.ViewModel.Items
{
    /// <summary>
    /// 虚空商人视图模型.
    /// </summary>
    public sealed class VoidTraderViewModel : ViewModelBase, ICountdownViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VoidTraderViewModel"/> class.
        /// </summary>
        public VoidTraderViewModel(VoidTrader data)
        {
            UpdateData(data);
            UpdateDataCommand = ReactiveCommand.Create<VoidTrader>(UpdateData);
            UpdateCountdownCommand = ReactiveCommand.Create(UpdateCountdown);
            ShowItemsCommand = ReactiveCommand.Create(ShowItems);
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
        /// 显示条目列表的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> ShowItemsCommand { get; }

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
            IsArrived = data.IsActive;
            HasItems = Data.Inventory?.Any() ?? false;
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

        private void ShowItems()
        {
            var appVM = Locator.Current.GetService<AppViewModel>();
            appVM.ShowVoidTraderItems(Data.Inventory);
        }
    }
}
