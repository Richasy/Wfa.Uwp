// Copyright (c) Richasy. All rights reserved.

namespace Wfa.Models.Enums
{
    /// <summary>
    /// 市场排序规则.
    /// </summary>
    public enum MarketSortType
    {
        /// <summary>
        /// 价格倒序（价高优先）.
        /// </summary>
        PriceDescending,

        /// <summary>
        /// 价格正序（价低优先）.
        /// </summary>
        PriceAscending,

        /// <summary>
        /// 增益属性倒序（增益属性多优先）.
        /// </summary>
        PositiveAttributeDescending,

        /// <summary>
        /// 增益属性倒序（增益属性少优先）.
        /// </summary>
        PositiveAttributeAscending,

        /// <summary>
        /// 伤害倒序（高伤害优先）.
        /// </summary>
        DamageDescending,

        /// <summary>
        /// 伤害正序（低伤害优先）.
        /// </summary>
        DamageAscending,
    }
}
