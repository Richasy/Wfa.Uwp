// Copyright (c) Richasy. All rights reserved.

using System.Threading.Tasks;

namespace Wfa.Provider.Interfaces
{
    /// <summary>
    /// 市场提供器的接口.
    /// </summary>
    public interface IMarketProvider
    {
        /// <summary>
        /// 更新商店条目列表.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        Task UpdateMarketItemsAsync();
    }
}
