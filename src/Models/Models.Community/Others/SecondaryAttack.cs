// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// 副武器攻击方式.
/// </summary>
public class SecondaryAttack : WeaponAttackBase
{
    /// <summary>
    /// 伤害.
    /// </summary>
    [JsonProperty("damage")]
    public SecondaryDamage? Damage { get; set; }
}
