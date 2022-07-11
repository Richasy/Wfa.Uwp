// Copyright (c) Richasy. All rights reserved.

using System.Collections.Generic;
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
        public List<VoidTraderItem> Inventory { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is VoidTrader trader && Id == trader.Id;

        /// <inheritdoc/>
        public override int GetHashCode() => Id.GetHashCode();
    }

    /// <summary>
    /// 虚空商人物品.
    /// </summary>
    public class VoidTraderItem
    {
        /// <summary>
        /// 物品名.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "item", Required = Required.Default)]
        public string Name { get; set; }

        /// <summary>
        /// 杜卡德金币.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "ducats", Required = Required.Default)]
        public int Ducats { get; set; }

        /// <summary>
        /// 现金.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "credits", Required = Required.Default)]
        public int Credits { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is VoidTraderItem inventory && Name == inventory.Name;

        /// <inheritdoc/>
        public override int GetHashCode() => Name.GetHashCode();
    }
}
