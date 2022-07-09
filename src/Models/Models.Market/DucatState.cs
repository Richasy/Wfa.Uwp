// Copyright (c) Richasy. All rights reserved.

using System;
using Newtonsoft.Json;

namespace Wfa.Models.Market
{
    /// <summary>
    /// 杜卡德散件信息.
    /// </summary>
    public class DucatState
    {
        /// <summary>
        /// 基础伤害.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "datetime", Required = Required.Default)]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 数量.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "volume", Required = Required.Default)]
        public int Count { get; set; }

        /// <summary>
        /// 杜卡德金币/白金.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "ducats_per_platform", Required = Required.Default)]
        public double DucatsPerPlatinum { get; set; }

        /// <summary>
        /// 杜卡德金币价值.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "ducats", Required = Required.Default)]
        public int Ducats { get; set; }

        /// <summary>
        /// 对应物品的 Id 值.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "item", Required = Required.Default)]
        public string ItemId { get; set; }

        /// <summary>
        /// 中位值.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "median", Required = Required.Default)]
        public double Median { get; set; }

        /// <summary>
        /// 加权平均价.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "wa_price", Required = Required.Default)]
        public float WaPrice { get; set; }

        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is DucatState state && ItemId == state.ItemId;

        /// <inheritdoc/>
        public override int GetHashCode() => ItemId.GetHashCode();
    }
}
