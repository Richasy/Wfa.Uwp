// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wfa.Models.State
{
    /// <summary>
    /// 阵营赏金任务概览.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public sealed class SyndicateMission : DurationEntityBase
    {
        /// <summary>
        /// 任务 Id.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// 是否正在活动.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "active", Required = Required.Default)]
        public bool Active { get; set; }

        /// <summary>
        /// 阵营名称.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "syndicate", Required = Required.Default)]
        public string Name { get; set; }

        /// <summary>
        /// 阵营标识.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "syndicateKey", Required = Required.Default)]
        public string Key { get; set; }

        /// <summary>
        /// 任务列表.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "jobs", Required = Required.Default)]
        public List<SyndicateJob> Jobs { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is SyndicateMission mission && Key == mission.Key;

        /// <inheritdoc/>
        public override int GetHashCode() => Key.GetHashCode();
    }

    /// <summary>
    /// 阵营赏金的细分工作.
    /// </summary>
    public class SyndicateJob
    {
        /// <summary>
        /// 工作 Id.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// 奖励池.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "rewardPool", Required = Required.Default)]
        public List<string> RewardPool { get; set; }

        /// <summary>
        /// 任务类型.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "type", Required = Required.Default)]
        public string Type { get; set; }

        /// <summary>
        /// 敌人等级的上限和下限.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "enemyLevels", Required = Required.Default)]
        public List<int> EnemyLevels { get; set; }

        /// <summary>
        /// 最低段位要求.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "minMR", Required = Required.Default)]
        public int MinMasteryRank { get; set; }

        /// <summary>
        /// 过期时间.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "expiry", Required = Required.Default)]
        public DateTime ExpiryTime { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is SyndicateJob job && Id == job.Id;

        /// <inheritdoc/>
        public override int GetHashCode() => Id.GetHashCode();
    }
}
