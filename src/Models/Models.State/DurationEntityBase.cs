// Copyright (c) Richasy. All rights reserved.

using System;
using Newtonsoft.Json;

namespace Wfa.Models.State
{
    /// <summary>
    /// 拥有计时时长的条目基类.
    /// </summary>
    public class DurationEntityBase
    {
        /// <summary>
        /// 开始时间.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "activation", Required = Required.Default)]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 过期时间.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "expiry", Required = Required.Default)]
        public DateTime ExpiryTime { get; set; }
    }
}
