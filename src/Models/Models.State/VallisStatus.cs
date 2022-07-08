// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.State
{
    /// <summary>
    /// 奥布山谷状态.
    /// </summary>
    public sealed class VallisStatus : DurationEntityBase
    {
        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// 是否为温暖状态.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "isWarm", Required = Required.Default)]
        public bool IsWarm { get; set; }

        /// <summary>
        /// 当前状态.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "state", Required = Required.Default)]
        public string State { get; set; }
    }
}
