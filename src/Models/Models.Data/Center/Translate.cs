// Copyright (c) Richasy. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;

namespace Wfa.Models.Data.Center
{
    /// <summary>
    /// 翻译条目.
    /// </summary>
    public sealed class Translate
    {
        /// <summary>
        /// 标识符.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 英文.
        /// </summary>
        public string En { get; set; }

        /// <summary>
        /// 中文.
        /// </summary>
        public string Zh { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Translate translate && En == translate.En;

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(En);
    }
}
