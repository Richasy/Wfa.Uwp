// Copyright (c) Richasy. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;

namespace Wfa.Models.Data.Center
{
    /// <summary>
    /// 全球化翻译.
    /// </summary>
    public sealed class Dict
    {
        /// <summary>
        /// 序号.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Id值.
        /// </summary>
        public string UniqueName { get; set; }

        /// <summary>
        /// Json内容.
        /// </summary>
        public string Content { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Dict dict && UniqueName == dict.UniqueName;

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(UniqueName);
    }
}
