// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.State
{
    /// <summary>
    /// 裂罅.
    /// </summary>
    public sealed class Fissure : DurationEntityBase
    {
        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// 是否正在进行.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "active", Required = Required.Default)]
        public bool IsActive { get; set; }

        /// <summary>
        /// 星球节点.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "node", Required = Required.Default)]
        public string Node { get; set; }

        /// <summary>
        /// 任务类型.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "missionType", Required = Required.Default)]
        public string MissionType { get; set; }

        /// <summary>
        /// 任务标识.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "missionKey", Required = Required.Default)]
        public string MissionKey { get; set; }

        /// <summary>
        /// 敌人阵营.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "enemy", Required = Required.Default)]
        public string Enemy { get; set; }

        /// <summary>
        /// 敌人阵营标识.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "enemyKey", Required = Required.Default)]
        public string EnemyKey { get; set; }

        /// <summary>
        /// 纪元.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "tier", Required = Required.Default)]
        public string Tier { get; set; }

        /// <summary>
        /// 纪元序号.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "tierNum", Required = Required.Default)]
        public int TierIndex { get; set; }

        /// <summary>
        /// 是否已经过期.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "expired", Required = Required.Default)]
        public bool IsExpired { get; set; }

        /// <summary>
        /// 是否正在风暴.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "isStorm", Required = Required.Default)]
        public bool IsStorm { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Fissure fissure && Id == fissure.Id;

        /// <inheritdoc/>
        public override int GetHashCode() => Id.GetHashCode();
    }
}
