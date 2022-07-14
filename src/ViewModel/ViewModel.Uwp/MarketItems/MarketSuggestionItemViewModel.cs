// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using Wfa.Models.Enums;
using Wfa.Models.Market;
using Wfa.ViewModel.Base;

namespace Wfa.ViewModel.MarketItems
{
    /// <summary>
    /// 商店建议条目视图模型.
    /// </summary>
    public sealed class MarketSuggestionItemViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarketSuggestionItemViewModel"/> class.
        /// </summary>
        public MarketSuggestionItemViewModel(ItemBase item, MarketDataType type = MarketDataType.Items)
        {
            Data = item;
            ItemType = type;
        }

        /// <summary>
        /// 数据.
        /// </summary>
        public ItemBase Data { get; set; }

        /// <summary>
        /// 条目类型.
        /// </summary>
        public MarketDataType ItemType { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is MarketSuggestionItemViewModel model && EqualityComparer<ItemBase>.Default.Equals(Data, model.Data);

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Data);
    }
}
