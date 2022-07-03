// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// 主武器攻击.
/// </summary>
public class PrimaryAttack : WeaponAttackBase
{
    /// <summary>
    /// 伤害.
    /// </summary>
    [JsonProperty("damage")]
    public PrimaryDamage? Damage { get; set; }
}
