// Copyright (c) Richasy. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Wfa.Models.Community;

/// <summary>
/// 条目基类.
/// </summary>
public class EntryBase
{
    /// <summary>
    /// 标识符.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 专有名.
    /// </summary>
    [JsonProperty("uniqueName")]
    public string UniqueName { get; set; }

    /// <summary>
    /// 物品名.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// 维基缩略图.
    /// </summary>
    [JsonProperty("wikiaThumbnail")]
    public string? WikiaThumbnail { get; set; }

    /// <summary>
    /// 维基地址.
    /// </summary>
    [JsonProperty("wikiaUrl")]
    public string? WikiaUrl { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is EntryBase @base && UniqueName == @base.UniqueName;

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(UniqueName);
}
