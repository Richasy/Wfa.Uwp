// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// 机甲基类.
/// </summary>
public class MechBase : EntryBase, IPolarities
{
    /// <summary>
    /// 护甲.
    /// </summary>
    [JsonProperty("armor")]
    public double Armor { get; set; }

    /// <summary>
    /// 分类.
    /// </summary>
    [JsonProperty("category")]
    public string Category { get; set; }

    /// <summary>
    /// 描述.
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; }

    /// <summary>
    /// 血量.
    /// </summary>
    [JsonProperty("health")]
    public double Health { get; set; }

    /// <summary>
    /// 段位需求.
    /// </summary>
    [JsonProperty("masteryReq")]
    public int MasteryReq { get; set; }

    /// <summary>
    /// 能量.
    /// </summary>
    [JsonProperty("power")]
    public double Power { get; set; }

    /// <summary>
    /// 产品分类.
    /// </summary>
    [JsonProperty("productCategory")]
    public string ProductCategory { get; set; }

    /// <summary>
    /// 护盾.
    /// </summary>
    [JsonProperty("shield")]
    public double Shield { get; set; }

    /// <summary>
    /// 冲刺速度.
    /// </summary>
    [JsonProperty("sprintSpeed")]
    public double SprintSpeed { get; set; }

    /// <summary>
    /// 耐力.
    /// </summary>
    [JsonProperty("stamina")]
    public double? Stamina { get; set; }

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
    /// 入库时间.
    /// </summary>
    [JsonProperty("vaultDate")]
    public string? VaultDate { get; set; }

    /// <summary>
    /// 是否入库.
    /// </summary>
    [JsonProperty("vaulted")]
    public bool? Vaulted { get; set; }

    /// <inheritdoc/>
    [JsonProperty("selfPolarities")]
    public string? SelfPolarities { get; set; }
}
