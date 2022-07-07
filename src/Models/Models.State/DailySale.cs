// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.State
{
    /// <summary>
    /// 每日商品（Darvo）.
    /// </summary>
    public sealed class DailySale : DurationEntityBase
    {
        /// <summary>
        /// 出售的武器名.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "item", Required = Required.Default)]
        public string Item { get; set; }

        /// <summary>
        /// 原价.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "originalPrice", Required = Required.Default)]
        public int OriginalPrice { get; set; }

        /// <summary>
        /// 折扣价.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "salePrice", Required = Required.Default)]
        public int SalePrice { get; set; }

        /// <summary>
        /// 总数.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "total", Required = Required.Default)]
        public int Total { get; set; }

        /// <summary>
        /// 已售出的个数.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "sold", Required = Required.Default)]
        public int Sold { get; set; }

        /// <summary>
        /// 标识符.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public string Id { get; set; }

        /// <summary>
        /// 折扣.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "discount", Required = Required.Default)]
        public int Discount { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is DailySale sale && Id == sale.Id;

        /// <inheritdoc/>
        public override int GetHashCode() => Id.GetHashCode();
    }
}
