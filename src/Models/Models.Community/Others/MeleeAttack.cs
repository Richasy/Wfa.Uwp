// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// 近战攻击.
/// </summary>
public class MeleeAttack : WeaponAttackBase
{
    /// <summary>
    /// 伤害.
    /// </summary>
    [JsonProperty("damage")]
    public MeleeDamage? Damage { get; set; }
}
