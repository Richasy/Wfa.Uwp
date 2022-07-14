// Copyright (c) Richasy. All rights reserved.

using Wfa.Models.Enums;

namespace Wfa.Models.Data.Local
{
    /// <summary>
    /// 商店条目类型标头.
    /// </summary>
    public sealed class MarketTypeHeader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarketTypeHeader"/> class.
        /// </summary>
        public MarketTypeHeader(MarketDataType type, string name)
        {
            Type = type;
            Name = name;
        }

        /// <summary>
        /// 数据类型.
        /// </summary>
        public MarketDataType Type { get; }

        /// <summary>
        /// 名称.
        /// </summary>
        public string Name { get; }
    }
}
