// Copyright (c) Richasy. All rights reserved.

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// 赋能效果.
/// </summary>
public class ArcaneEffect
{
    /// <summary>
    /// 名称.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 等级.
    /// </summary>
    [JsonProperty("rank")]
    public int Rank { get; set; }

    /// <summary>
    /// 描述.
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; }
}
