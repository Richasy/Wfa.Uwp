// Copyright (c) Richasy. All rights reserved.

using System.Threading.Tasks;

namespace Wfa.Provider.Interfaces
{
    /// <summary>
    /// 世界状态提供器接口.
    /// </summary>
    public interface IStateProvider
    {
        /// <summary>
        /// 缓存当前世界状态.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        Task CacheWorldStateAsync();
    }
}
