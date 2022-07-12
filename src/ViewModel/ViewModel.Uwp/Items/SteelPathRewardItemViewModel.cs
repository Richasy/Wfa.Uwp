// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using ReactiveUI.Fody.Helpers;
using Wfa.Models.State;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.Items
{
    /// <summary>
    /// 钢铁之路奖励条目视图模型.
    /// </summary>
    public sealed class SteelPathRewardItemViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SteelPathRewardItemViewModel"/> class.
        /// </summary>
        /// <param name="data">数据.</param>
        /// <param name="isSelected">是否被选中.</param>
        public SteelPathRewardItemViewModel(SteelPathReward data, bool isSelected)
        {
            IsSelected = isSelected;
            Data = data;
        }

        /// <summary>
        /// 是否已选择.
        /// </summary>
        [Reactive]
        public bool IsSelected { get; set; }

        /// <summary>
        /// 源数据.
        /// </summary>
        [Reactive]
        public SteelPathReward Data { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is SteelPathRewardItemViewModel model && EqualityComparer<SteelPathReward>.Default.Equals(Data, model.Data);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Data);
    }
}
