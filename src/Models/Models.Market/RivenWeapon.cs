// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.Market
{
    /// <summary>
    /// 紫卡武器条目.
    /// </summary>
    public sealed class RivenWeapon : ItemBase
    {
        /// <summary>
        /// 紫卡类型.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "riven_type", Required = Required.Default)]
        public string RivenType { get; set; }

        /// <summary>
        /// 分组.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "group", Required = Required.Default)]
        public string Group { get; set; }
    }
}
