// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// 空战武器攻击描述.
/// </summary>
public class ArchGunAttack : WeaponAttackBase
{
    /// <summary>
    /// 伤害.
    /// </summary>
    [JsonProperty("damage")]
    public ArchGunDamage? Damage { get; set; }
}
