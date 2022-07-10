// Copyright (c) Richasy. All rights reserved.

using System;
using Newtonsoft.Json;

namespace Wfa.Models.State
{
    /// <summary>
    /// 事件信息.
    /// </summary>
    public class Event
    {
        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// 启动时间.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "activation", Required = Required.Default)]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 过期时间.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "expiry", Required = Required.Default)]
        public DateTime ExpiryTime { get; set; }

        /// <summary>
        /// 是否正处于活动之中.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "active", Required = Required.Default)]
        public bool IsActive { get; set; }

        /// <summary>
        /// 活动名称.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description", Required = Required.Default)]
        public string Name { get; set; }

        /// <summary>
        /// 描述内容.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "tooltip", Required = Required.Default)]
        public string Description { get; set; }

        /// <summary>
        /// 活动的星球节点.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "node", Required = Required.Default)]
        public string Node { get; set; }

        /// <summary>
        /// 是否已经过期.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "expired", Required = Required.Default)]
        public bool IsExpired { get; set; }

        /// <summary>
        /// 当前活动进度（x/100）.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "health", Required = Required.Default)]
        public double Progress { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Event @event && Name == @event.Name;

        /// <inheritdoc/>
        public override int GetHashCode() => Name.GetHashCode();
    }
}
