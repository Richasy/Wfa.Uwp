// Copyright (c) Richasy. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Wfa.Models.Data.Center;

/// <summary>
/// Warframe Market 物品条目.
/// </summary>
public class MarketItem
{
    /// <summary>
    /// 标识符.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 代码.
    /// </summary>
    [JsonProperty("code")]
    public string Code { get; set; }

    /// <summary>
    /// 物品主体.
    /// </summary>
    [JsonProperty("main")]
    public string Main { get; set; }

    /// <summary>
    /// 中文.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// 缩略图.
    /// </summary>
    [JsonProperty("thumbnail")]
    public string Thumbnail { get; set; }

    /// <summary>
    /// 商店Id.
    /// </summary>
    [JsonProperty("marketId")]
    public string MarketId { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj) => obj is MarketItem sale && MarketId == sale.MarketId;

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(MarketId);
}
