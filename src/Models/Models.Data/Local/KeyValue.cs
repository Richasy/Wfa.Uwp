// Copyright (c) Richasy. All rights reserved.

using System;

namespace Wfa.Models.Data.Local
{
    /// <summary>
    /// 键值对.
    /// </summary>
    public sealed class KeyValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValue"/> class.
        /// </summary>
        public KeyValue(string key, string value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// 名称.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 值.
        /// </summary>
        public string Value { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is KeyValue value && Key == value.Key;

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(Key);
    }
}
