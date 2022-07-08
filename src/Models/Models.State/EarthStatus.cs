// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.State
{
    /// <summary>
    /// 地球状态.
    /// </summary>
    public sealed class EarthStatus : DurationEntityBase
    {
        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// 是否为白天.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "isDay", Required = Required.Default)]
        public bool IsDay { get; set; }

        /// <summary>
        /// 当前状态.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "state", Required = Required.Default)]
        public string State { get; set; }
    }
}
