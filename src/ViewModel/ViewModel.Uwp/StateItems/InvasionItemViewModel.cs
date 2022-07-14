// Copyright (c) Richasy. All rights reserved.

using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.Enums;
using Wfa.Models.State;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.StateItems
{
    /// <summary>
    /// 入侵条目视图模型.
    /// </summary>
    public sealed class InvasionItemViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvasionItemViewModel"/> class.
        /// </summary>
        public InvasionItemViewModel(Invasion data)
        {
            UpdateData(data);
            UpdateDataCommand = ReactiveCommand.Create<Invasion>(UpdateData);
        }

        /// <summary>
        /// 更新数据命令.
        /// </summary>
        public ReactiveCommand<Invasion, Unit> UpdateDataCommand { get; }

        /// <summary>
        /// 进攻方图标.
        /// </summary>
        [Reactive]
        public WfaSymbol AttackerSymbol { get; set; }

        /// <summary>
        /// 防守方图标.
        /// </summary>
        [Reactive]
        public WfaSymbol DefenderSymbol { get; set; }

        /// <summary>
        /// 入侵进度.
        /// </summary>
        [Reactive]
        public double InvasionProgress { get; set; }

        /// <summary>
        /// 进攻方奖励.
        /// </summary>
        [Reactive]
        public string AttackerReward { get; set; }

        /// <summary>
        /// 防守方奖励.
        /// </summary>
        [Reactive]
        public string DefenderReward { get; set; }

        /// <summary>
        /// 进攻方名称.
        /// </summary>
        [Reactive]
        public string AttackerName { get; set; }

        /// <summary>
        /// 防守方名称.
        /// </summary>
        [Reactive]
        public string DefenderName { get; set; }

        /// <summary>
        /// 是否为感染者爆发.
        /// </summary>
        [Reactive]
        public bool IsInfested { get; set; }

        /// <summary>
        /// 节点.
        /// </summary>
        [Reactive]
        public string Node { get; set; }

        /// <summary>
        /// 标识符.
        /// </summary>
        public string Id { get; set; }

        private void UpdateData(Invasion data)
        {
            Id = data.Id;
            IsInfested = data.IsVsInfestation;

            AttackerName = data.Attacker.Faction;
            DefenderName = data.Defender.Faction;

            AttackerSymbol = GetFactionSymbol(data.Attacker.Faction);
            DefenderSymbol = GetFactionSymbol(data.Defender.Faction);

            AttackerReward = data.Attacker.Reward?.Content ?? "--";
            DefenderReward = data.Defender.Reward.Content;

            InvasionProgress = data.Progress;
            Node = data.Node;
        }

        private WfaSymbol GetFactionSymbol(string faction)
        {
            return faction switch
            {
                "Grineer" => WfaSymbol.Grineer,
                "Corpus" => WfaSymbol.Corpus,
                _ => WfaSymbol.Infested,
            };
        }
    }
}
