// Copyright (c) Richasy. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// 战甲技能.
/// </summary>
public class Ability
{
    /// <summary>
    /// 标识符.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 技能名.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// 描述.
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Ability ability && Id == ability.Id;

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(Id);
}
