// Copyright (c) Richasy. All rights reserved.

using System;
using Newtonsoft.Json;

namespace Wfa.Models.Market
{
    /// <summary>
    /// 条目订单.
    /// </summary>
    public sealed class ItemOrder
    {
        /// <summary>
        /// 白金.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "platinum", Required = Required.Default)]
        public int Platinum { get; set; }

        /// <summary>
        /// 数量.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "quantity", Required = Required.Default)]
        public int Quantity { get; set; }

        /// <summary>
        /// 订单类型.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "order_type", Required = Required.Default)]
        public string OrderType { get; set; }

        /// <summary>
        /// 用户信息.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "user", Required = Required.Default)]
        public MarketUser User { get; set; }

        /// <summary>
        /// 所属平台.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "platform", Required = Required.Default)]
        public string Platform { get; set; }

        /// <summary>
        /// 国家地区代码.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "region", Required = Required.Default)]
        public string Region { get; set; }

        /// <summary>
        /// 创建时间.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "creation_date", Required = Required.Default)]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// 上次更新时间.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "last_update", Required = Required.Default)]
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 订单是否可见.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "visible", Required = Required.Default)]
        public bool IsVisible { get; set; }

        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// Mod 等级.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "mod_rank", Required = Required.Default)]
        public int? ModRank { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is ItemOrder order && Id == order.Id;

        /// <inheritdoc/>
        public override int GetHashCode() => Id.GetHashCode();
    }
}
