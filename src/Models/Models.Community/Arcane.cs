// Copyright (c) Richasy. All rights reserved.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// 赋能.
/// </summary>
public class Arcane : EntryBase
{
    /// <summary>
    /// 分类.
    /// </summary>
    [JsonProperty("category")]
    public string Category { get; set; }

    /// <summary>
    /// 图片名.
    /// </summary>
    [JsonProperty("imageName")]
    public string? ImageName { get; set; }

    /// <summary>
    /// 赋能名.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// 是否可交易.
    /// </summary>
    [JsonProperty("tradable")]
    public bool Tradable { get; set; }

    /// <summary>
    /// 类型.
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    /// 分类.
    /// </summary>
    [JsonProperty("arcaneEffect")]
    public List<ArcaneEffect> Effects { get; set; }

    /// <summary>
    /// 稀有度.
    /// </summary>
    [JsonProperty("rarity")]
    public string? Rarity { get; set; }
}
