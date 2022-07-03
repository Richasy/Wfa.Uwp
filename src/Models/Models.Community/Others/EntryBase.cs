﻿// Copyright (c) Richasy. All rights reserved.

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

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is EntryBase @base && UniqueName == @base.UniqueName;

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(UniqueName);
}
