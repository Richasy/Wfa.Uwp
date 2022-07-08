// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.Market
{
    /// <summary>
    /// 玄骸幻纹.
    /// </summary>
    public sealed class LichEphemera
    {
        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// 幻纹标识.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "url_name", Required = Required.Default)]
        public string Identifier { get; set; }

        /// <summary>
        /// 幻纹名.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "item_name", Required = Required.Default)]
        public string Name { get; set; }

        /// <summary>
        /// 幻纹图标.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "thumb", Required = Required.Default)]
        public string Thumb { get; set; }

        /// <summary>
        /// 动图.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "animation", Required = Required.Default)]
        public string Animation { get; set; }

        /// <summary>
        /// 元素.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "element", Required = Required.Default)]
        public string Element { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is LichEphemera ephemera && Id == ephemera.Id;

        /// <inheritdoc/>
        public override int GetHashCode() => Id.GetHashCode();
    }
}
