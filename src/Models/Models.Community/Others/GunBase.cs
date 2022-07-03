// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// 枪械武器基类.
/// </summary>
public class GunBase : EntryBase, IPolarities
{
    /// <summary>
    /// 精准度.
    /// </summary>
    [JsonProperty("accuracy")]
    public double Accuracy { get; set; }

    /// <summary>
    /// 弹药量.
    /// </summary>
    [JsonProperty("ammo")]
    public double? Ammo { get; set; }

    /// <summary>
    /// 分类.
    /// </summary>
    [JsonProperty("category")]
    public string? Category { get; set; }

    /// <summary>
    /// 暴击几率.
    /// </summary>
    [JsonProperty("criticalChance")]
    public double CriticalChance { get; set; }

    /// <summary>
    /// 暴击倍率.
    /// </summary>
    [JsonProperty("criticalMultiplier")]
    public double CriticalMultiplier { get; set; }

    /// <summary>
    /// 描述.
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; }

    /// <summary>
    /// 裂罅倾向性.
    /// </summary>
    [JsonProperty("disposition")]
    public int Disposition { get; set; }

    /// <summary>
    /// 射速.
    /// </summary>
    [JsonProperty("fireRate")]
    public double FireRate { get; set; }

    /// <summary>
    /// 图片名.
    /// </summary>
    [JsonProperty("imageName")]
    public string? ImageName { get; set; }

    /// <summary>
    /// 弹匣容量.
    /// </summary>
    [JsonProperty("magazineSize")]
    public int MagazineSize { get; set; }

    /// <summary>
    /// 段位要求.
    /// </summary>
    [JsonProperty("masteryReq")]
    public int MasteryReq { get; set; }

    /// <summary>
    /// 多重射击.
    /// </summary>
    [JsonProperty("multishot")]
    public double Multishot { get; set; }

    /// <summary>
    /// 武器名称.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// 噪音等级.
    /// </summary>
    [JsonProperty("noise")]
    public string Noise { get; set; }

    /// <summary>
    /// 裂罅倾向数值.
    /// </summary>
    [JsonProperty("omegaAttenuation")]
    public double OmegaAttenuation { get; set; }

    /// <summary>
    /// 触发几率.
    /// </summary>
    [JsonProperty("procChance")]
    public double ProcChance { get; set; }

    /// <summary>
    /// 产品分类.
    /// </summary>
    [JsonProperty("productCategory")]
    public string? ProductCategory { get; set; }

    /// <summary>
    /// 发布时间.
    /// </summary>
    [JsonProperty("releaseDate")]
    public string? ReleaseDate { get; set; }

    /// <summary>
    /// 装填时间（秒）.
    /// </summary>
    [JsonProperty("reloadTime")]
    public double ReloadTime { get; set; }

    /// <summary>
    /// 总伤害.
    /// </summary>
    [JsonProperty("totalDamage")]
    public double TotalDamage { get; set; }

    /// <summary>
    /// 是否可交易.
    /// </summary>
    [JsonProperty("tradable")]
    public bool Tradable { get; set; }

    /// <summary>
    /// 触发方式.
    /// </summary>
    [JsonProperty("trigger")]
    public string? Trigger { get; set; }

    /// <summary>
    /// 武器类型.
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    /// 维基缩略图.
    /// </summary>
    [JsonProperty("wikiaThumbnail")]
    public string? WikiaThumbnail { get; set; }

    /// <summary>
    /// 维基地址.
    /// </summary>
    [JsonProperty("wikiaUrl")]
    public string? WikiaUrl { get; set; }

    /// <inheritdoc/>
    [JsonProperty("selfPolarities")]
    public string? SelfPolarities { get; set; }
}
