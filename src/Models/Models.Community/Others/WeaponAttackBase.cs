// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// 武器攻击基类.
/// </summary>
public class WeaponAttackBase : AttackBase
{
    /// <summary>
    /// 蓄力时间.
    /// </summary>
    [JsonProperty("charge_time")]
    public double? ChargeTime { get; set; }

    /// <summary>
    /// 射击类型.
    /// </summary>
    [JsonProperty("shot_type")]
    public string? ShotType { get; set; }

    /// <summary>
    /// 投射物飞行速度.
    /// </summary>
    [JsonProperty("flight")]
    public double? Flight { get; set; }
}
