﻿// Copyright (c) Richasy. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Wfa.Models.Data.Center;

/// <summary>
/// 元数据.
/// </summary>
public class Meta
{
    /// <summary>
    /// 标识符.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 名称.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// 值.
    /// </summary>
    [JsonProperty("value")]
    public string Value { get; set; }

    /// <inheritdoc/>
    public override bool Equals(object obj) => obj is Meta meta && Name == meta.Name;

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(Name);
}
