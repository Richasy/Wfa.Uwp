// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
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
    /// 裂缝条目视图模型.
    /// </summary>
    public sealed class FissureItemViewModel : ViewModelBase, ICountdownViewModel
    {
        private DateTime _expiryTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="FissureItemViewModel"/> class.
        /// </summary>
        public FissureItemViewModel(Fissure data)
        {
            UpdateData(data);
            UpdateCountdownCommand = ReactiveCommand.Create(UpdateCountdown);
            UpdateDataCommand = ReactiveCommand.Create<Fissure>(UpdateData);
        }

        /// <inheritdoc/>
        public ReactiveCommand<Unit, Unit> UpdateCountdownCommand { get; }

        /// <summary>
        /// 更新数据的命令.
        /// </summary>
        public ReactiveCommand<Fissure, Unit> UpdateDataCommand { get; }

        /// <inheritdoc/>
        [Reactive]
        public string Countdown { get; set; }

        /// <summary>
        /// 图标.
        /// </summary>
        [Reactive]
        public WfaSymbol Symbol { get; set; }

        /// <summary>
        /// 源数据.
        /// </summary>
        [Reactive]
        public Fissure Data { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is FissureItemViewModel model && EqualityComparer<Fissure>.Default.Equals(Data, model.Data);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Data);

        private void UpdateData(Fissure data)
        {
            Data = data;
            _expiryTime = data.ExpiryTime.ToLocalTime();
            Symbol = data.EnemyKey switch
            {
                "Grineer" => WfaSymbol.Grineer,
                "Corpus" => WfaSymbol.Corpus,
                "Infested" => WfaSymbol.Infested,
                "Orokin" => WfaSymbol.Orokin,
                _ => WfaSymbol.Fissure,
            };

            UpdateCountdown();
        }

        private void UpdateCountdown()
        {
            if (_expiryTime == DateTime.MinValue
                || _expiryTime <= DateTime.Now)
            {
                Countdown = TimeSpan.Zero.Humanize(minUnit: Humanizer.Localisation.TimeUnit.Second);
                return;
            }

            var endFormat = Locator.Current.GetService<IResourceToolkit>().GetLocaleString(LanguageNames.EndDateFormat);
            Countdown = string.Format(endFormat, _expiryTime.Humanize());
        }
    }
}
