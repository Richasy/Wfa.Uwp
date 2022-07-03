// Copyright (c) Richasy. All rights reserved.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// 主武器.
/// </summary>
public class Primary : GunBase
{
    /// <summary>
    /// 攻击方式.
    /// </summary>
    [JsonProperty("attacks")]
    public List<PrimaryAttack> Attacks { get; set; }

    /// <summary>
    /// 等级上限.
    /// </summary>
    [JsonProperty("maxLevelCap")]
    public int MaxLevelCap { get; set; }
}
