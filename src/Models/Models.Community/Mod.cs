// Copyright (c) Richasy. All rights reserved.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// Mod.
/// </summary>
public class Mod : EntryBase
{
    /// <summary>
    /// 基础容量.
    /// </summary>
    [JsonProperty("baseDrain")]
    public int? BaseDrain { get; set; }

    /// <summary>
    /// 分类.
    /// </summary>
    [JsonProperty("category")]
    public string Category { get; set; }

    /// <summary>
    /// 适用对象.
    /// </summary>
    [JsonProperty("compatName")]
    public string? CompatName { get; set; }

    /// <summary>
    /// 最高等级.
    /// </summary>
    [JsonProperty("fusionLimit")]
    public int FusionLimit { get; set; }

    /// <summary>
    /// 是否为光环.
    /// </summary>
    [JsonProperty("isAugment")]
    public bool? IsAugment { get; set; }

    /// <summary>
    /// 分类.
    /// </summary>
    [JsonProperty("modEffects")]
    public List<ModEffect> Effects { get; set; }

    /// <summary>
    /// 极性.
    /// </summary>
    [JsonProperty("polarity")]
    public string? Polarity { get; set; }

    /// <summary>
    /// 稀有度.
    /// </summary>
    [JsonProperty("rarity")]
    public string? Rarity { get; set; }

    /// <summary>
    /// 发布时间.
    /// </summary>
    [JsonProperty("releaseDate")]
    public string? ReleaseDate { get; set; }

    /// <summary>
    /// 是否可交易.
    /// </summary>
    [JsonProperty("tradable")]
    public bool? Tradable { get; set; }

    /// <summary>
    /// 是否可以通过转换获得.
    /// </summary>
    [JsonProperty("transmutable")]
    public bool? Transmutable { get; set; }

    /// <summary>
    /// 物品类型.
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    /// 是否为增益卡.
    /// </summary>
    [JsonProperty("isUtility")]
    public bool? IsUtility { get; set; }

    /// <summary>
    /// 所属模组.
    /// </summary>
    [JsonProperty("modSet")]
    public string? ModSet { get; set; }

    /// <summary>
    /// 是否为特殊功能卡.
    /// </summary>
    [JsonProperty("isExilus")]
    public bool? IsExilus { get; set; }

    /// <summary>
    /// Mod组合增益增幅比例.
    /// </summary>
    [JsonProperty("modSetEffects")]
    public string? ModSetEffects { get; set; }
}
