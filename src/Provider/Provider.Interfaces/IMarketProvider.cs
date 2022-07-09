// Copyright (c) Richasy. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;
using Wfa.Models.Enums;
using Wfa.Models.Market;

namespace Wfa.Provider.Interfaces
{
    /// <summary>
    /// 市场提供器的接口.
    /// </summary>
    public interface IMarketProvider
    {
        /// <summary>
        /// 更新市场数据.
        /// </summary>
        /// <param name="dataType">数据类型.</param>
        /// <returns>更新是否成功.</returns>
        Task<bool> UpdateDataAsync(MarketDataType dataType);

        /// <summary>
        /// 提交更新时间.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        Task CommitUpdateTimeAsync();

        /// <summary>
        /// 获取物品订单列表.
        /// </summary>
        /// <param name="itemIdentifier">物品标识符.</param>
        /// <returns>订单列表.</returns>
        Task<IEnumerable<ItemOrder>> GetItemOrdersAsync(string itemIdentifier);

        /// <summary>
        /// 获取紫卡拍卖订单.
        /// </summary>
        /// <param name="itemIdentifier">紫卡武器标识.</param>
        /// <param name="buyoutPolicy">销售策略.</param>
        /// <param name="positiveStats">增益属性集.</param>
        /// <param name="negativeStats">减益属性集.</param>
        /// <param name="minMasteryRank">最低段位需求.</param>
        /// <param name="maxMasteryRank">最高段位需求.</param>
        /// <param name="minReroll">最低重置次数.</param>
        /// <param name="maxReroll">最高重置次数.</param>
        /// <param name="modRank">紫卡等级类型.</param>
        /// <param name="polarity">紫卡极性.</param>
        /// <param name="sortType">排序.</param>
        /// <returns>紫卡拍卖订单.</returns>
        Task<IEnumerable<AuctionRivenOrder>> GetRivenOrdersAsync(
            string itemIdentifier,
            MarketBuyoutPolicy buyoutPolicy,
            IEnumerable<string> positiveStats,
            IEnumerable<string> negativeStats,
            int minMasteryRank,
            int maxMasteryRank,
            int minReroll,
            int maxReroll,
            RivenRankType modRank,
            RivenModPolarity polarity,
            MarketSortType sortType);

        /// <summary>
        /// 获取玄骸拍卖订单.
        /// </summary>
        /// <param name="itemIdentifier">订单标识符.</param>
        /// <param name="buyoutPolicy">销售策略.</param>
        /// <param name="elementIdentifier">元素标识符.</param>
        /// <param name="hasEphemera">是否带幻纹.</param>
        /// <param name="minDamage">最低伤害.</param>
        /// <param name="maxDamage">最高伤害.</param>
        /// <param name="quirkIdentifier">怪癖标识符.</param>
        /// <param name="sortType">排序类型.</param>
        /// <returns>玄骸拍卖订单.</returns>
        Task<IEnumerable<AuctionLichOrder>> GetLichOrdersAsync(
            string itemIdentifier,
            MarketBuyoutPolicy buyoutPolicy,
            string elementIdentifier,
            bool hasEphemera,
            int minDamage,
            int maxDamage,
            string quirkIdentifier,
            MarketSortType sortType);

        /// <summary>
        /// 获取杜卡德散件榜单信息.
        /// </summary>
        /// <returns>杜卡德散件榜单.</returns>
        Task<DucatsPayload> GetDucatsInformationAsync();
    }
}
