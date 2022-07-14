// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.Market
{
    /// <summary>
    /// 条目基类.
    /// </summary>
    public class ItemBase
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
        /// 武器名.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "item_name", Required = Required.Default)]
        public string Name { get; set; }

        /// <summary>
        /// 武器图标.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "thumb", Required = Required.Default)]
        public string Thumb { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is ItemBase weapon && Id == weapon.Id;

        /// <inheritdoc/>
        public override int GetHashCode() => Id.GetHashCode();
    }
}
