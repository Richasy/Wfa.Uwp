// Copyright (c) Richasy. All rights reserved.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// 战甲.
/// </summary>
public class Warframe : MechBase
{
    /// <summary>
    /// 技能.
    /// </summary>
    [JsonProperty("abilities")]
    public List<WarframeAbility> Abilities { get; set; }

    /// <summary>
    /// 光环极性.
    /// </summary>
    [JsonProperty("aura")]
    public string? Aura { get; set; }

    /// <summary>
    /// 武形秘仪中可用.
    /// </summary>
    [JsonProperty("conclave")]
    public bool Conclave { get; set; }

    /// <summary>
    /// 被动技能描述.
    /// </summary>
    [JsonProperty("passiveDescription")]
    public string? PassiveDescription { get; set; }

    /// <summary>
    /// 性别.
    /// </summary>
    [JsonProperty("sex")]
    public string? Sex { get; set; }
}
