// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.Market
{
    /// <summary>
    /// 紫卡属性.
    /// </summary>
    public sealed class RivenAttribute
    {
        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// 武器标识.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "url_name", Required = Required.Default)]
        public string Identifier { get; set; }

        /// <summary>
        /// 分组.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "group", Required = Required.Default)]
        public string Group { get; set; }

        /// <summary>
        /// 前缀.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "prefix", Required = Required.Default)]
        public string Prefix { get; set; }

        /// <summary>
        /// 后缀.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "suffix", Required = Required.Default)]
        public string Suffix { get; set; }

        /// <summary>
        /// 正属性是减益属性.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "positive_is_negative", Required = Required.Default)]
        public bool PositiveIsNegative { get; set; }

        /// <summary>
        /// 专属的武器类型（用,分隔）.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "exclusive_to", Required = Required.Default)]
        public string ExclusiveTo { get; set; }

        /// <summary>
        /// 增益类型.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "effect", Required = Required.Default)]
        public string Effect { get; set; }

        /// <summary>
        /// 增益单位，可以是百分比 precent，或者秒 seconds 等.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "units", Required = Required.Default)]
        public string Unit { get; set; }

        /// <summary>
        /// 是否仅为减益效果.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "negative_only", Required = Required.Default)]
        public bool IsNegativeOnly { get; set; }

        /// <summary>
        /// 是否仅用于搜索.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "search_only", Required = Required.Default)]
        public bool IsSearchOnly { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is RivenAttribute attribute && Id == attribute.Id;

        /// <inheritdoc/>
        public override int GetHashCode() => Id.GetHashCode();
    }
}
