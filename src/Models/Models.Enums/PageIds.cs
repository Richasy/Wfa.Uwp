// Copyright (c) Richasy. All rights reserved.

namespace Wfa.Models.Enums
{
    /// <summary>
    /// 页面 Id.
    /// </summary>
    /// <remarks>
    /// 1-100: 应用及世界状态页面.
    /// 100-199: 市场页面.
    /// 200-299: 资料页面.
    /// </remarks>
    public enum PageIds
    {
        /// <summary>
        /// 空白页面.
        /// </summary>
        None = 0,

        /// <summary>
        /// 帮助页面.
        /// </summary>
        Help = 1,

        /// <summary>
        /// 设置页面.
        /// </summary>
        Settings = 2,

        /// <summary>
        /// 常用世界状态首页.
        /// </summary>
        WorldStateHome = 3,

        /// <summary>
        /// 资料展示页面.
        /// </summary>
        Library = 4,

        /// <summary>
        /// 工具页.
        /// </summary>
        Tools = 5,

        /// <summary>
        /// 赏金奖励页面.
        /// </summary>
        SyndicateMissions = 6,

        /// <summary>
        /// 入侵页面.
        /// </summary>
        Invasions = 7,

        /// <summary>
        /// 午夜电波页面.
        /// </summary>
        Nightwave = 8,

        /// <summary>
        /// 虚空裂缝页面.
        /// </summary>
        Fissure = 9,

        /// <summary>
        /// Warframe Market 条目订单页面.
        /// </summary>
        MarketItemOrder = 100,

        /// <summary>
        /// Warframe Market 玄骸页面.
        /// </summary>
        MarketLichOrder = 101,

        /// <summary>
        /// Warframe Market 紫卡页面.
        /// </summary>
        MarketRivenOrder = 102,

        /// <summary>
        /// Warframe Market 提供的杜卡德金币比值页面.
        /// </summary>
        MarketDucats = 103,

        /// <summary>
        /// 友链页面.
        /// </summary>
        FriendLinks = 200,

        /// <summary>
        /// 中英翻译页面.
        /// </summary>
        Translate = 201,

        /// <summary>
        /// 资料库详情页.
        /// </summary>
        LibraryDetail = 202,
    }
}
