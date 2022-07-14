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
    /// 突击视图模型.
    /// </summary>
    public sealed class SortieViewModel : ViewModelBase, ICountdownViewModel
    {
        private DateTime _expiryTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="SortieViewModel"/> class.
        /// </summary>
        public SortieViewModel(Sortie data)
        {
            Variants = new ObservableCollection<SortieVariant>();
            UpdateCountdownCommand = ReactiveCommand.Create(UpdateCountdown);
            UpdateDataCommand = ReactiveCommand.Create<Sortie>(UpdateData);
            UpdateData(data);
        }

        /// <summary>
        /// 更新数据命令.
        /// </summary>
        public ReactiveCommand<Sortie, Unit> UpdateDataCommand { get; }

        /// <summary>
        /// 更新倒计时的命令.
        /// </summary>
        public ReactiveCommand<Unit, Unit> UpdateCountdownCommand { get; }

        /// <summary>
        /// 任务列表.
        /// </summary>
        public ObservableCollection<SortieVariant> Variants { get; }

        /// <summary>
        /// 过期提示.
        /// </summary>
        [Reactive]
        public string Countdown { get; set; }

        /// <summary>
        /// 阵营标识.
        /// </summary>
        [Reactive]
        public WfaSymbol Symbol { get; set; }

        /// <summary>
        /// 阵营名.
        /// </summary>
        [Reactive]
        public string FactionName { get; set; }

        /// <summary>
        /// Boss 名称.
        /// </summary>
        [Reactive]
        public string BossName { get; set; }

        /// <summary>
        /// 标识符.
        /// </summary>
        public string Id { get; set; }

        private void UpdateData(Sortie data)
        {
            FactionName = data.Faction;
            BossName = data.Boss;
            Symbol = data.Faction switch
            {
                "Grineer" => WfaSymbol.Grineer,
                "Corpus" => WfaSymbol.Corpus,
                "Infested" => WfaSymbol.Infested,
                "Infestation" => WfaSymbol.Infested,
                "Orokin" => WfaSymbol.Orokin,
                _ => WfaSymbol.Sortie,
            };

            _expiryTime = data.ExpiryTime.ToLocalTime();

            if (Id != data.Id)
            {
                Id = data.Id;
                var newsCount = data.Variants.Count(data => !Variants.Contains(data));
                if (newsCount > 0)
                {
                    TryClear(Variants);
                    data.Variants.ForEach(p => Variants.Add(p));
                }
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
