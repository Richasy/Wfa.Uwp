// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wfa.Models.State
{
    /// <summary>
    /// 入侵.
    /// </summary>
    public sealed class Invasion
    {
        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// 开始时间.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "activation", Required = Required.Default)]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 星球节点.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "node", Required = Required.Default)]
        public string Node { get; set; }

        /// <summary>
        /// 进攻方信息.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "attacker", Required = Required.Default)]
        public InvasionFaction Attacker { get; set; }

        /// <summary>
        /// 防守方信息.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "defender", Required = Required.Default)]
        public InvasionFaction Defender { get; set; }

        /// <summary>
        /// 是否是与感染者战斗.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "vsInfestation", Required = Required.Default)]
        public bool IsVsInfestation { get; set; }

        /// <summary>
        /// 完成进度.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "completion", Required = Required.Default)]
        public double Progress { get; set; }

        /// <summary>
        /// 是否已经完成.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "completed", Required = Required.Default)]
        public bool IsCompleted { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Invasion invasion && Id == invasion.Id;

        /// <inheritdoc/>
        public override int GetHashCode() => Id.GetHashCode();
    }

    /// <summary>
    /// 入侵任务中的阵营信息.
    /// </summary>
    public class InvasionFaction
    {
        /// <summary>
        /// 入侵奖励.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "reward", Required = Required.Default)]
        public InvasionReward Reward { get; set; }

        /// <summary>
        /// 阵营名称.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "faction", Required = Required.Default)]
        public string Faction { get; set; }
    }

    /// <summary>
    /// 入侵奖励.
    /// </summary>
    public class InvasionReward
    {
        /// <summary>
        /// 奖励内容.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "node", Required = Required.Default)]
        public List<InvasionCountedItem> Items { get; set; }

        /// <summary>
        /// 现金奖励.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "credits", Required = Required.Default)]
        public int Credits { get; set; }

        /// <summary>
        /// 奖励内容的可读文本.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "itemString", Required = Required.Default)]
        public string Content { get; set; }

        /// <summary>
        /// 奖励缩略图.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "thumbnail", Required = Required.Default)]
        public string Thumbnail { get; set; }
    }

    /// <summary>
    /// 入侵奖励条目.
    /// </summary>
    public class InvasionCountedItem
    {
        /// <summary>
        /// 个数.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "count", Required = Required.Default)]
        public int Count { get; set; }

        /// <summary>
        /// 物品名称.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "type", Required = Required.Default)]
        public string Name { get; set; }

        /// <summary>
        /// 物品标识.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "key", Required = Required.Default)]
        public string Key { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is InvasionCountedItem item && Key == item.Key;

        /// <inheritdoc/>
        public override int GetHashCode() => Key.GetHashCode();
    }
}
