// Copyright (c) Richasy. All rights reserved.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// 空战武器.
/// </summary>
public class ArchMelee : MeleeBase
{
    /// <summary>
    /// 攻击方式.
    /// </summary>
    [JsonProperty("attacks")]
    public List<ArchMeleeAttack> Attacks { get; set; }
}
