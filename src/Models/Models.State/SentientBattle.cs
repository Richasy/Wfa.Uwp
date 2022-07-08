// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.State
{
    /// <summary>
    /// Sentient 战舰（九重天）.
    /// </summary>
    public sealed class SentientBattle : DurationEntityBase
    {
        /// <summary>
        /// 任务信息.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "mission", Required = Required.Default)]
        public SentientMission Mission { get; set; }

        /// <summary>
        /// 是否正在进行.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "active", Required = Required.Default)]
        public bool IsActive { get; set; }

        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }
    }

    /// <summary>
    /// Sentient 战舰任务（九重天）.
    /// </summary>
    public class SentientMission
    {
        /// <summary>
        /// 星球节点.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "node", Required = Required.Default)]
        public string Node { get; set; }

        /// <summary>
        /// 阵营.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "faction", Required = Required.Default)]
        public string Faction { get; set; }

        /// <summary>
        /// 任务类型.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "type", Required = Required.Default)]
        public string Type { get; set; }
    }
}
