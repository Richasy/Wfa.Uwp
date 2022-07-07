// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wfa.Models.State
{
    /// <summary>
    /// 钢铁之路信息.
    /// </summary>
    public sealed class SteelPath : DurationEntityBase
    {
        /// <summary>
        /// 当前的奖励.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "currentReward", Required = Required.Default)]
        public SteelPathReward CurrentReward { get; set; }

        /// <summary>
        /// 轮次奖励.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "rotation", Required = Required.Default)]
        public List<SteelPathReward> Rotation { get; set; }

        /// <summary>
        /// 常驻奖励.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "evergreens", Required = Required.Default)]
        public List<SteelPathReward> Evergreens { get; set; }

        /// <summary>
        /// 侵袭信息.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "incursions", Required = Required.Default)]
        public SteelPathIncursions Incursions { get; set; }
    }

    /// <summary>
    /// 钢铁之路奖励.
    /// </summary>
    public class SteelPathReward
    {
        /// <summary>
        /// 奖励条目.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "name", Required = Required.Default)]
        public string Name { get; set; }

        /// <summary>
        /// 消耗.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "cost", Required = Required.Default)]
        public int Cost { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is SteelPathReward reward && Name == reward.Name;

        /// <inheritdoc/>
        public override int GetHashCode() => Name.GetHashCode();
    }

    /// <summary>
    /// 钢铁之路的侵袭信息.
    /// </summary>
    public class SteelPathIncursions : DurationEntityBase
    {
        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }
    }
}
