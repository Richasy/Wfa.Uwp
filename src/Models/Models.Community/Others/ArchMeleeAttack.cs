// Copyright (c) Richasy. All rights reserved.

using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// 空战武器攻击.
/// </summary>
public class ArchMeleeAttack : AttackBase
{
    /// <summary>
    /// 伤害.
    /// </summary>
    [JsonProperty("damage")]
    public ArchMeleeDamage? Damage { get; set; }
}
