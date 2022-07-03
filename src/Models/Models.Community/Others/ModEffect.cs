// Copyright (c) Richasy. All rights reserved.

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// Mod 效果.
/// </summary>
public class ModEffect
{
    /// <summary>
    /// 名称.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 等级.
    /// </summary>
    [JsonProperty("level")]
    public int Level { get; set; }

    /// <summary>
    /// 描述.
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; }
}
