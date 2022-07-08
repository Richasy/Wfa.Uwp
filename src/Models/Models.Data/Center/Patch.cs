// Copyright (c) Richasy. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;

namespace Wfa.Models.Data.Center
{
    /// <summary>
    /// 增补翻译，适用于简体 -> 繁体.
    /// </summary>
    public sealed class Patch
    {
        /// <summary>
        /// 标识符.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 原名.
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// 简体中文.
        /// </summary>
        public string Chs { get; set; }

        /// <summary>
        /// 繁体中文.
        /// </summary>
        public string Cht { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Patch patch && Origin == patch.Origin;

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Origin);
    }
}
