// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.State
{
    /// <summary>
    /// 虚空商人.
    /// </summary>
    public sealed class VoidTrader : DurationEntityBase
    {
        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// 商人是否已经抵达.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "active", Required = Required.Default)]
        public bool IsActive { get; set; }

        /// <summary>
        /// 商人名称.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "character", Required = Required.Default)]
        public string Character { get; set; }

        /// <summary>
        /// 地点.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "location", Required = Required.Default)]
        public string Location { get; set; }

        /// <summary>
        /// 商品清单.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "inventory", Required = Required.Default)]
        public object[] Inventory { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is VoidTrader trader && Id == trader.Id;

        /// <inheritdoc/>
        public override int GetHashCode() => Id.GetHashCode();
    }
}
