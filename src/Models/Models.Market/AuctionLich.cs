// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.Market
{
    /// <summary>
    /// 拍卖的玄骸.
    /// </summary>
    public class AuctionLich
    {
        /// <summary>
        /// 武器标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "weapon_url_name", Required = Required.Default)]
        public string Identifier { get; set; }

        /// <summary>
        /// 基础伤害.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "damage", Required = Required.Default)]
        public double Damage { get; set; }

        /// <summary>
        /// 是否拥有幻纹.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "having_ephemera", Required = Required.Default)]
        public bool HavingEphemera { get; set; }

        /// <summary>
        /// 物品类型.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "type", Required = Required.Default)]
        public string Type { get; set; }

        /// <summary>
        /// 怪癖.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "quirk", Required = Required.Default)]
        public string Quirk { get; set; }

        /// <summary>
        /// 元素信息.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "element", Required = Required.Default)]
        public string Element { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is AuctionLich lich && Identifier == lich.Identifier;

        /// <inheritdoc/>
        public override int GetHashCode() => Identifier.GetHashCode();
    }
}
