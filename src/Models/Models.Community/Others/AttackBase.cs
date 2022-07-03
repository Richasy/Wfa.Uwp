// Copyright (c) Richasy. All rights reserved.

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// 伤害基类.
/// </summary>
public abstract class AttackBase
{
    /// <summary>
    /// 标识符.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 名称.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// 攻击速度.
    /// </summary>
    [JsonProperty("speed")]
    public double? Speed { get; set; }

    /// <summary>
    /// 暴击几率.
    /// </summary>
    [JsonProperty("crit_chance")]
    public double? CritChance { get; set; }

    /// <summary>
    /// 暴击倍率.
    /// </summary>
    [JsonProperty("crit_mult")]
    public double? CritMult { get; set; }

    /// <summary>
    /// 触发几率.
    /// </summary>
    [JsonProperty("status_chance")]
    public double? StatusChance { get; set; }
}
