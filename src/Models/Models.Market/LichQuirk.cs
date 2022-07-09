// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.Market
{
    /// <summary>
    /// 玄骸怪癖.
    /// </summary>
    public sealed class LichQuirk
    {
        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// 怪癖标识.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "url_name", Required = Required.Default)]
        public string Identifier { get; set; }

        /// <summary>
        /// 怪癖名.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "item_name", Required = Required.Default)]
        public string Name { get; set; }

        /// <summary>
        /// 描述.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description", Required = Required.Default)]
        public string Description { get; set; }

        /// <summary>
        /// 分组.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "group", Required = Required.Default)]
        public string Group { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is LichQuirk quirk && Id == quirk.Id;

        /// <inheritdoc/>
        public override int GetHashCode() => Id.GetHashCode();
    }
}
