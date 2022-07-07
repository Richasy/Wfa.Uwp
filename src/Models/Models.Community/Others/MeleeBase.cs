// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// 近战武器基类.
/// </summary>
public class MeleeBase : EntryBase, IPolarities
{
    /// <summary>
    /// 武器格挡角度.
    /// </summary>
    [JsonProperty("blockingAngle")]
    public int BlockingAngle { get; set; }

    /// <summary>
    /// 分类.
    /// </summary>
    [JsonProperty("category")]
    public string Category { get; set; }

    /// <summary>
    /// 组合技完成时间.
    /// </summary>
    [JsonProperty("comboDuration")]
    public int ComboDuration { get; set; }

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
    public string? Description { get; set; }

    /// <summary>
    /// 裂罅倾向星级.
    /// </summary>
    [JsonProperty("disposition")]
    public int Disposition { get; set; }

    /// <summary>
    /// 攻击速度.
    /// </summary>
    [JsonProperty("fireRate")]
    public double FireRate { get; set; }

    /// <summary>
    /// 重击伤害.
    /// </summary>
    [JsonProperty("heavyAttackDamage")]
    public double HeavyAttackDamage { get; set; }

    /// <summary>
    /// 重击震地伤害.
    /// </summary>
    [JsonProperty("heavySlamAttack")]
    public double HeavySlamAttack { get; set; }

    /// <summary>
    /// 图片名.
    /// </summary>
    [JsonProperty("imageName")]
    public string? ImageName { get; set; }

    /// <summary>
    /// 段位要求.
    /// </summary>
    [JsonProperty("masteryReq")]
    public int MasteryReq { get; set; }

    /// <summary>
    /// 裂罅倾向值.
    /// </summary>
    [JsonProperty("omegaAttenuation")]
    public double OmegaAttenuation { get; set; }

    /// <inheritdoc/>
    [JsonProperty("selfPolarities")]
    public string? SelfPolarities { get; set; }

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
    /// 攻击范围.
    /// </summary>
    [JsonProperty("range")]
    public double Range { get; set; }

    /// <summary>
    /// 震地攻击.
    /// </summary>
    [JsonProperty("slamAttack")]
    public double SlamAttack { get; set; }

    /// <summary>
    /// 震地范围伤害.
    /// </summary>
    [JsonProperty("slamRadialDamage")]
    public double SlamRadialDamage { get; set; }

    /// <summary>
    /// 震地半径.
    /// </summary>
    [JsonProperty("slamRadius")]
    public double SlamRadius { get; set; }

    /// <summary>
    /// 滑行攻击.
    /// </summary>
    [JsonProperty("slideAttack")]
    public double SlideAttack { get; set; }

    /// <summary>
    /// 总伤害.
    /// </summary>
    [JsonProperty("totalDamage")]
    public double? TotalDamage { get; set; }

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
    /// 维基缩略图.
    /// </summary>
    [JsonProperty("wikiaThumbnail")]
    public string? WikiaThumbnail { get; set; }

    /// <summary>
    /// 维基网址.
    /// </summary>
    [JsonProperty("wikiaUrl")]
    public string? WikiaUrl { get; set; }
}
