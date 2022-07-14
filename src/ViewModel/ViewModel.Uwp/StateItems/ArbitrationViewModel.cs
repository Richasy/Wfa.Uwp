// Copyright (c) Richasy. All rights reserved.

using System;
using System.Reactive;
using Humanizer;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.Enums;
using Wfa.Models.State;
using Wfa.ViewModel.Base;
using Wfa.ViewModel.Interfaces;

namespace Wfa.ViewModel.StateItems
{
    /// <summary>
    /// 仲裁视图模型.
    /// </summary>
    public sealed class ArbitrationViewModel : ViewModelBase, ICountdownViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArbitrationViewModel"/> class.
        /// </summary>
        public ArbitrationViewModel(Arbitration data)
        {
            UpdateData(data);
            UpdateDataCommand = ReactiveCommand.Create<Arbitration>(UpdateData);
            UpdateCountdownCommand = ReactiveCommand.Create(UpdateCountdown);
        }

        /// <inheritdoc/>
        public ReactiveCommand<Unit, Unit> UpdateCountdownCommand { get; }

        /// <summary>
        /// 更新数据的命令.
        /// </summary>
        public ReactiveCommand<Arbitration, Unit> UpdateDataCommand { get; }

        /// <inheritdoc/>
        [Reactive]
        public string Countdown { get; set; }

        /// <summary>
        /// 数据.
        /// </summary>
        [Reactive]
        public Arbitration Data { get; set; }

        /// <summary>
        /// 数据是否为空.
        /// </summary>
        [Reactive]
        public bool IsEmpty { get; set; }

        /// <summary>
        /// 阵营标识.
        /// </summary>
        [Reactive]
        public WfaSymbol FactionSymbol { get; set; }

        private void UpdateData(Arbitration data)
        {
            Data = data;
            IsEmpty = string.IsNullOrEmpty(data.Type);
            FactionSymbol = data.Enemy switch
            {
                "Infested" => WfaSymbol.Infested,
                "Grineer" => WfaSymbol.Grineer,
                "Corpus" => WfaSymbol.Corpus,
                "Orokin" => WfaSymbol.Orokin,
                _ => WfaSymbol.Melee,
            };

            UpdateCountdown();
        }

        private void UpdateCountdown()
        {
            if (Data == null)
            {
                return;
            }

            var expiryTime = Data.ExpiryTime.ToLocalTime();
            if (expiryTime < DateTime.Now)
            {
                Countdown = TimeSpan.Zero.Humanize(minUnit: Humanizer.Localisation.TimeUnit.Second);
                return;
            }

            var ts = expiryTime - DateTime.Now;
            Countdown = ts.Humanize(maxUnit: Humanizer.Localisation.TimeUnit.Hour, minUnit: Humanizer.Localisation.TimeUnit.Second);
        }
    }
}
