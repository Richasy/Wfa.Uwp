// Copyright (c) Richasy. All rights reserved.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// 空战装甲.
/// </summary>
public class Archwing : MechBase
{
    /// <summary>
    /// 技能.
    /// </summary>
    [JsonProperty("abilities")]
    public List<ArchwingAbility> Abilities { get; set; }
}
