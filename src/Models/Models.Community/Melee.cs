// Copyright (c) Richasy. All rights reserved.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// 近战武器.
/// </summary>
public class Melee : MeleeBase
{
    /// <summary>
    /// 攻击类型.
    /// </summary>
    [JsonProperty("attacks")]
    public List<MeleeAttack> Attacks { get; set; }

    /// <summary>
    /// 重击震地范围伤害.
    /// </summary>
    [JsonProperty("heavySlamRadialDamage")]
    public double? HeavySlamRadialDamage { get; set; }

    /// <summary>
    /// 重击震地范围.
    /// </summary>
    [JsonProperty("heavySlamRadius")]
    public double? HeavySlamRadius { get; set; }

    /// <summary>
    /// 发布时间.
    /// </summary>
    [JsonProperty("releaseDate")]
    public string? ReleaseDate { get; set; }

    /// <summary>
    /// 光环极性.
    /// </summary>
    [JsonProperty("stancePolarity")]
    public string? StancePolarity { get; set; }

    /// <summary>
    /// 在重击之前的蓄力时间.
    /// </summary>
    [JsonProperty("windUp")]
    public double? WindUp { get; set; }

    /// <summary>
    /// 等级上限.
    /// </summary>
    [JsonProperty("maxLevelCap")]
    public int? MaxLevelCap { get; set; }
}
