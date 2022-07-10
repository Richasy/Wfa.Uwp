// Copyright (c) Richasy. All rights reserved.

using System;
using System.Reactive;
using Humanizer;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Wfa.Models.State;
using Wfa.Toolkit.Interfaces;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.Items
{
    /// <summary>
    /// 每日折扣视图模型.
    /// </summary>
    public sealed class DailySaleViewModel : ViewModelBase
    {
        private DateTime _expiryTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="DailySaleViewModel"/> class.
        /// </summary>
        public DailySaleViewModel(DailySale data)
        {
            UpdateCountdownCommand = ReactiveCommand.Create(UpdateCountdown);
            UpdateDataCommand = ReactiveCommand.Create<DailySale>(UpdateData);
            UpdateData(data);
        }

        /// <summary>
        /// 更新数据命令.
        /// </summary>
        public ReactiveCommand<DailySale, Unit> UpdateDataCommand { get; }

        /// <summary>
        /// 更新倒计时的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> UpdateCountdownCommand { get; }

        /// <summary>
        /// 过期提示.
        /// </summary>
        [Reactive]
        public string ExpiryTip { get; set; }

        /// <summary>
        /// 剩余数量.
        /// </summary>
        [Reactive]
        public int Remaining { get; set; }

        /// <summary>
        /// 源数据.
        /// </summary>
        [Reactive]
        public DailySale Data { get; set; }

        private void UpdateData(DailySale data)
        {
            Data = data;
            Remaining = Data.Total - Data.Sold;
            _expiryTime = data.ExpiryTime.ToLocalTime();
            UpdateCountdown();
        }

        private void UpdateCountdown()
        {
            if (_expiryTime == DateTime.MinValue
                || _expiryTime <= DateTime.Now)
            {
                ExpiryTip = "--";
                return;
            }

            var expiryFormat = Locator.Current.GetService<IResourceToolkit>().GetLocaleString(Models.Enums.LanguageNames.ExpiryDateFormat);
            ExpiryTip = string.Format(expiryFormat, _expiryTime.Humanize());
        }
    }
}
