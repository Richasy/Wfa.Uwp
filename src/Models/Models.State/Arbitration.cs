// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.State
{
    /// <summary>
    /// 仲裁信息.
    /// </summary>
    public sealed class Arbitration : DurationEntityBase
    {
        /// <summary>
        /// 敌人阵营.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "enemy", Required = Required.Default)]
        public string Enemy { get; set; }

        /// <summary>
        /// 任务类型.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "type", Required = Required.Default)]
        public string Type { get; set; }

        /// <summary>
        /// 是否为空战.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "archwing", Required = Required.Default)]
        public bool IsArchwing { get; set; }

        /// <summary>
        /// 是否为水战.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "sharkwing", Required = Required.Default)]
        public bool IsSharkwing { get; set; }

        /// <summary>
        /// 星球节点.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "node", Required = Required.Default)]
        public string Node { get; set; }

        /// <summary>
        /// 星球节点标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "nodeKey", Required = Required.Default)]
        public string NodeKey { get; set; }

        /// <summary>
        /// 任务类型标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "typeKey", Required = Required.Default)]
        public string TypeKey { get; set; }

        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// 是否已经过期.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "expired", Required = Required.Default)]
        public bool IsExpired { get; set; }
    }
}
