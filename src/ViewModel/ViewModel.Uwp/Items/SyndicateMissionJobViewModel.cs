// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.ObjectModel;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.Enums;
using Wfa.Models.State;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.Items
{
    /// <summary>
    /// 赏金单项任务视图模型.
    /// </summary>
    public sealed class SyndicateMissionJobViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SyndicateMissionJobViewModel"/> class.
        /// </summary>
        public SyndicateMissionJobViewModel(SyndicateJob job, WfaSymbol symbol)
        {
            EnemyLevel = string.Join("~", job.EnemyLevels);
            MinMasteryRank = job.MinMasteryRank;
            Name = job.Type;
            Id = job.Id;
            Symbol = symbol;
            Rewards = new ObservableCollection<string>(job.RewardPool);
        }

        /// <summary>
        /// 敌人等级.
        /// </summary>
        [Reactive]
        public string EnemyLevel { get; set; }

        /// <summary>
        /// 最低段位要求.
        /// </summary>
        [Reactive]
        public int MinMasteryRank { get; set; }

        /// <summary>
        /// 任务名.
        /// </summary>
        [Reactive]
        public string Name { get; set; }

        /// <summary>
        /// 图标.
        /// </summary>
        [Reactive]
        public WfaSymbol Symbol { get; set; }

        /// <summary>
        /// 标识符.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 奖励列表.
        /// </summary>
        public ObservableCollection<string> Rewards { get; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is SyndicateMissionJobViewModel model && Id == model.Id;

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Id);
    }
}
